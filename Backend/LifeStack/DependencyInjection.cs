using Application;
using Domain;
using Infrastructure;
using Infrastructure.Authentication;
using Infrastructure.Authorization;

namespace Web.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI().
                AddInfrastructureDI(configuration)
                .AddDomainDI(configuration)
                .AuthenticationDI(configuration)
                .AuthorizationDI();

            services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            return services;
        }
    }
}
