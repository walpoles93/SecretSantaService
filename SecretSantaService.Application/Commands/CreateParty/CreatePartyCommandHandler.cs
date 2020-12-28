using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretSantaService.Application.Common.Interfaces;
using SecretSantaService.Domain.Pairings;
using SecretSantaService.Domain.Parties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecretSantaService.Application.Commands.CreateParty
{
    public class CreatePartyCommandHandler : IRequestHandler<CreatePartyCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IPairingFactory _pairingFactory;
        private readonly IEmailer _emailer;

        public CreatePartyCommandHandler(IApplicationDbContext dbContext, IPairingFactory pairingFactory, IEmailer emailer)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _pairingFactory = pairingFactory ?? throw new ArgumentNullException(nameof(pairingFactory));
            _emailer = emailer ?? throw new ArgumentNullException(nameof(emailer));
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

            // create a pairing for each party member
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

            // save pairing to db
            _dbContext.Pairings.AddRange(pairings);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // only send emails if saving to db was successful
            foreach (var pairing in pairings)
            {
                var donor = partyMembers.FirstOrDefault(pm => pm.Id == pairing.DonorId);
                var recipient = partyMembers.FirstOrDefault(pm => pm.Id == pairing.RecipientId);

                await SendPairingEmail(donor, recipient, pairing, cancellationToken);
            }
        }

        private async Task SendPairingEmail(PartyMember donor, PartyMember recipient, Pairing pairing, CancellationToken cancellationToken)
        {
            var party = await _dbContext.Parties
                .Where(p => p.Id == pairing.PartyId)
                .Select(p => new { p.Name, p.Date })
                .FirstOrDefaultAsync(cancellationToken);
            var body = new StringBuilder();
            body.Append($"<p>You have been added to a Secret Santa party ({party.Name}) taking place on {party.Date:dd/MM/yyyy}</p>");
            body.Append($"<p>Your recipient is: {recipient.Name}</p>");
            body.Append($"<p>Please send your gift to: {recipient.Address}</p>");

            await _emailer.Send(donor.Email, "You have been added to a Secret Santa party", body.ToString());
        }
    }
}
