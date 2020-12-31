using FluentValidation;
using SecretSantaService.Application.Common.Interfaces;
using SecretSantaService.Application.Common.Services;

namespace SecretSantaService.Application.Queries.ViewPartyPairings
{
    public class ViewPartyPairingsQueryValidator : RequestValidator<ViewPartyPairingsQuery>
    {
        public ViewPartyPairingsQueryValidator(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void ExecuteRules()
        {
            RuleFor(q => q.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
