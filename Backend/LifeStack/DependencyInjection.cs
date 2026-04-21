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
                AddInfrastructureDI(configuration)
                .AddDomainDI(configuration);

            services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
            return services;
        }
    }
}
