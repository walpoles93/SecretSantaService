using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretSantaService.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecretSantaService.Application.Queries.ViewPartyPairings
{
    public class ViewPartyPairingsQueryHandler : IRequestHandler<ViewPartyPairingsQuery, ViewPartyPairingsDto>
    {
        private IApplicationDbContext _dbContext;

        public ViewPartyPairingsQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ViewPartyPairingsDto> Handle(ViewPartyPairingsQuery request, CancellationToken cancellationToken)
        {
            var partyId = await _dbContext.Parties
                .Where(p => p.Name == request.Name)
                .Select(p => p.Id)
                .FirstOrDefaultAsync(cancellationToken);
            var partyMembers = await _dbContext.PartyMembers
                .Where(pm => pm.PartyId == partyId)
                .Select(pm => new { pm.Id, pm.Name })
                .ToListAsync(cancellationToken);
            var pairings = await _dbContext.Pairings
                .Where(p => p.PartyId == partyId)
                .Select(p => new { p.DonorId, p.RecipientId })
                .ToListAsync(cancellationToken);

            var pairingsDto = new List<ViewPartyPairingsDto.PairingDto>();
            foreach (var pairing in pairings)
            {
                var donor = partyMembers.FirstOrDefault(pm => pm.Id == pairing.DonorId);
                var recipient = partyMembers.FirstOrDefault(pm => pm.Id == pairing.RecipientId);

                pairingsDto.Add(new ViewPartyPairingsDto.PairingDto
                {
                    DonorName = donor.Name,
                    RecipientName = recipient.Name
                });
            }

            return new ViewPartyPairingsDto(request.Name, pairingsDto);
        }
    }
}
