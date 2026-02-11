using Application;
using Domain;
using Infrastructure;

namespace Web.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI().
                AddInfrastructureDI()
                .AddDomainDI(configuration);
            return services;
        }
    }
}
