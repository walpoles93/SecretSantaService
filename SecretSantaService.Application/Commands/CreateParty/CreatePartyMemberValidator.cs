using FluentValidation;
using SecretSantaService.Application.Common.Interfaces;
using SecretSantaService.Application.Common.Services;

namespace SecretSantaService.Application.Commands.CreateParty
{
    public class CreatePartyMemberValidator : RequestValidator<CreatePartyCommand.CreatePartyMember>
    {
        public CreatePartyMemberValidator(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override void ExecuteRules()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.Address)
                .NotNull()
                .NotEmpty();
        }
    }
}
