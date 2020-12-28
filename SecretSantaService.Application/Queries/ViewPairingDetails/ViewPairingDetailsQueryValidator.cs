using FluentValidation;
using SecretSantaService.Application.Common.Interfaces;
using SecretSantaService.Application.Common.Services;

namespace SecretSantaService.Application.Queries.ViewPairingDetails
{
    public class ViewPairingDetailsValidator : RequestValidator<ViewPairingDetailsQuery>
    {
        public ViewPairingDetailsValidator(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void ExecuteRules()
        {
            RuleFor(q => q.PartyName)
                .NotNull()
                .NotEmpty();

            RuleFor(q => q.PairingIdentifier)
                .NotNull()
                .NotEmpty();
        }
    }
}
