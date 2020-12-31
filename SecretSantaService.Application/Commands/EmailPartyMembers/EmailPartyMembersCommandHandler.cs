using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretSantaService.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecretSantaService.Application.Commands.EmailPartyMembers
{
    public class EmailPartyMembersCommandHandler : IRequestHandler<EmailPartyMembersCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IEmailer _emailer;

        public EmailPartyMembersCommandHandler(IApplicationDbContext dbContext, IEmailer emailer)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _emailer = emailer ?? throw new ArgumentNullException(nameof(emailer));
        }

        public async Task<Unit> Handle(EmailPartyMembersCommand request, CancellationToken cancellationToken)
        {
            var party = await _dbContext.Parties
                .Where(p => p.Name == request.PartyName)
                .Select(p => new { p.Id, p.Name, p.Date })
                .FirstOrDefaultAsync(cancellationToken);
            var partyMembers = await _dbContext.PartyMembers
                .Where(pm => pm.PartyId == party.Id)
                .Select(pm => new { pm.Id, pm.Name, pm.Address, pm.Email })
                .ToListAsync(cancellationToken);
            var pairings = await _dbContext.Pairings
                .Where(p => p.PartyId == party.Id)
                .Select(p => new { p.DonorId, p.RecipientId })
                .ToListAsync(cancellationToken);

            foreach (var partyMember in partyMembers)
            {
                var pairing = pairings.FirstOrDefault(p => p.DonorId == partyMember.Id);
                var recipient = partyMembers.FirstOrDefault(pm => pm.Id == pairing.RecipientId);

                var body = new StringBuilder();
                body.Append($"<p>You have been added to a Secret Santa party ({party.Name}) taking place on {party.Date:dd/MM/yyyy}</p>");
                body.Append($"<p>Your recipient is: {recipient.Name}</p>");
                body.Append($"<p>Please send your gift to: {recipient.Address}</p>");

                await _emailer.Send(partyMember.Email, "You have been added to a Secret Santa party", body.ToString());
            }

            return Unit.Value;
        }
    }
}
