using FluentValidation;
using SecretSantaService.Application.Common.Interfaces;
using System;

namespace SecretSantaService.Application.Common.Services
{
    public abstract class RequestValidator<TRequest> : AbstractValidator<TRequest>
    {
        protected RequestValidator(IApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            ExecuteRules();
        }

        protected IApplicationDbContext DbContext { get; }

        protected abstract void ExecuteRules();
    }
}
