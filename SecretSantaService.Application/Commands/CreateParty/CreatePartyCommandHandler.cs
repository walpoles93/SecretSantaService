using MediatR;
using SecretSantaService.Application.Common.Interfaces;
using SecretSantaService.Domain.Pairings;
using SecretSantaService.Domain.Parties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecretSantaService.Application.Commands.CreateParty
{
    public class CreatePartyCommandHandler : IRequestHandler<CreatePartyCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IPairingFactory _pairingFactory;

        public CreatePartyCommandHandler(IApplicationDbContext dbContext, IPairingFactory pairingFactory)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _pairingFactory = pairingFactory ?? throw new ArgumentNullException(nameof(pairingFactory));
        }

        public async Task<Unit> Handle(CreatePartyCommand request, CancellationToken cancellationToken)
        {
            var party = new Party(request.Name, request.Date);
            foreach (var partyMember in request.PartyMembers)
                party.AddPartyMember(partyMember.Name, partyMember.Email, partyMember.Address);

            _dbContext.Parties.Add(party);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await CreatePairings(party.PartyMembers, cancellationToken);

            return Unit.Value;
        }

        private async Task CreatePairings(IEnumerable<PartyMember> partyMembers, CancellationToken cancellationToken)
        {
            var rng = new Random();
            var pairings = new List<Pairing>();
            var remainingRecipientIds = partyMembers.Select(pm => pm.Id).ToList();

            foreach (var partyMember in partyMembers)
            {
                int recipientId;
                int randomIndex;

                // ensure party member doesnt pick themselves
                do
                {
                    randomIndex = rng.Next(remainingRecipientIds.Count);
                    recipientId = remainingRecipientIds[randomIndex];
                } while (recipientId == partyMember.Id);

                remainingRecipientIds.RemoveAt(randomIndex);

                var pairing = _pairingFactory.CreatePairing(partyMember.PartyId, partyMember.Id, recipientId);
                pairings.Add(pairing);
            }

            _dbContext.Pairings.AddRange(pairings);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
