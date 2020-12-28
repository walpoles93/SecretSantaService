using Microsoft.Extensions.DependencyInjection;
using SecretSantaService.Domain.Common.Interfaces;
using SecretSantaService.Domain.Common.Services;
using SecretSantaService.Domain.Pairings;

namespace SecretSantaService.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services.AddServices();
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IShortUuidGenerator, ShortUuidGenerator>();
            services.AddTransient<IPairingFactory, PairingFactory>();

            return services;
        }
    }
}
