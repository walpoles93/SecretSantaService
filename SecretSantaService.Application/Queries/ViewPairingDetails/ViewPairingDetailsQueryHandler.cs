using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretSantaService.Application.Common.Exceptions;
using SecretSantaService.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecretSantaService.Application.Queries.ViewPairingDetails
{
    public class ViewPairingDetailsQueryHandler : IRequestHandler<ViewPairingDetailsQuery, ViewPairingDetailsDto>
    {
        private readonly IApplicationDbContext _dbContext;

        public ViewPairingDetailsQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ViewPairingDetailsDto> Handle(ViewPairingDetailsQuery request, CancellationToken cancellationToken)
        {
            var party = await _dbContext.Parties
                .Where(p => p.Name == request.PartyName)
                .Select(p => new { p.Id, p.Name, p.Date })
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundSecretSantaException($"Party with name: {request.PartyName} not found");

            var pairing = await _dbContext.Pairings
                    .Where(p => p.PartyId == party.Id)
                    .Where(p => p.Identifier == request.PairingIdentifier)
                    .Select(p => new { p.DonorId, p.RecipientId })
                    .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new NotFoundSecretSantaException($"Pairing with identifier: {request.PairingIdentifier} and partyId: {party.Id} not found");

            var donor = await _dbContext.PartyMembers
                .Where(pm => pm.Id == pairing.DonorId)
                .Select(pm => new { pm.Name })
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundSecretSantaException($"Party member with Id: {pairing.DonorId} not found");

            var recipient = await _dbContext.PartyMembers
                .Where(pm => pm.Id == pairing.RecipientId)
                .Select(pm => new { pm.Name, pm.Address })
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundSecretSantaException($"Party member with Id: {pairing.RecipientId} not found");

            return new ViewPairingDetailsDto
            {
                PartyName = party.Name,
                PartyDate = party.Date,
                DonorName = donor.Name,
                RecipientName = recipient.Name,
                RecipientAddress = recipient.Address,
            };
        }
    }
}
