﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SecretSantaService.Application.Common.Interfaces;
using SecretSantaService.Application.Common.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
                .NotEmpty()
                .MustAsync(NameExists);
        }

        private async Task<bool> NameExists(string name, CancellationToken cancellation)
        {
            return await DbContext.Parties
                .Select(p => p.Name)
                .ContainsAsync(name);
        }
    }
}
