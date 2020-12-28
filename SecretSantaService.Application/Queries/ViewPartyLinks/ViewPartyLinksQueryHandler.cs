using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretSantaService.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecretSantaService.Application.Queries.ViewPartyLinks
{
    public class ViewPartyLinksQueryHandler : IRequestHandler<ViewPartyLinksQuery, ViewPartyLinksDto>
    {
        private readonly IApplicationDbContext _dbContext;

        public ViewPartyLinksQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ViewPartyLinksDto> Handle(ViewPartyLinksQuery request, CancellationToken cancellationToken)
        {
            var partyId = await _dbContext.Parties
                .Where(p => p.Name == request.PartyName)
                .Select(p => p.Id)
                .FirstOrDefaultAsync(cancellationToken);
            var partyMembers = await _dbContext.PartyMembers
                .Where(pm => pm.PartyId == partyId)
                .Select(pm => new { pm.Id, pm.Name })
                .ToListAsync(cancellationToken);

            var dtos = new List<ViewPartyLinksDto.ViewPartyMember>();
            foreach (var member in partyMembers)
            {
                var pairingIdentifier = await _dbContext.Pairings
                    .Where(p => p.DonorId == member.Id)
                    .Select(p => p.Identifier)
                    .FirstOrDefaultAsync(cancellationToken);

                dtos.Add(new ViewPartyLinksDto.ViewPartyMember
                {
                    Name = member.Name,
                    PartyName = request.PartyName,
                    PairingIdentifier = pairingIdentifier
                });
            }

            return new ViewPartyLinksDto(dtos);
        }
    }
}
