using FluentValidation;
using SecretSantaService.Application.Common.Interfaces;
using SecretSantaService.Application.Common.Services;

namespace SecretSantaService.Application.Queries.ViewPartyLinks
{
    public class ViewPartyLinksValidator : RequestValidator<ViewPartyLinksQuery>
    {
        public ViewPartyLinksValidator(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void ExecuteRules()
        {
            RuleFor(q => q.PartyName)
                .NotNull()
                .NotEmpty();
        }
    }
}
