using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SecretSantaService.Application.Common.Interfaces;
using SecretSantaService.Application.Common.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecretSantaService.Application.Commands.CreateParty
{
    public class CreatePartyCommandValidator : RequestValidator<CreatePartyCommand>
    {
        public CreatePartyCommandValidator(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void ExecuteRules()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MustAsync(BeUniqueName);

            RuleFor(c => c.Date)
                .GreaterThanOrEqualTo(DateTime.UtcNow);

            RuleFor(c => c.PartyMembers)
                .NotNull()
                .Must(pm => pm.Count() >= 2);

            RuleForEach(c => c.PartyMembers).SetValidator(new CreatePartyMemberValidator(DbContext)); // TODO use DI
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return !await DbContext.Parties
                .Select(p => p.Name)
                .ContainsAsync(name, cancellationToken);
        }
    }
}
