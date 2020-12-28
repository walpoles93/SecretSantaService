using Microsoft.Extensions.DependencyInjection;

namespace SecretSantaService.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWeb(this IServiceCollection services)
        {
            return services
                .AddHelpers();
        }

        private static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddSpaStaticFiles(options => options.RootPath = "client-app/dist");

            return services;
        }
    }
}
