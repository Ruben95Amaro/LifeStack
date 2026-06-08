using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Authorization
{

  public static class DependencyInjection
    {
        public static IServiceCollection AuthorizationDI(this IServiceCollection services)
        {
            services.AddAuthorization();
            return services;
        }
    }
}
