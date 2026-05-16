using Domain.Users.Entities;
using Domain.Users.Interfaces;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructureDI(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddDatabase(configuration)
            .AddIdentityRoles()
            .AddPolicy()
            .AddHealthChecks(configuration);

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(
                options => options
                    .UseNpgsql(connectionString, npgsqlOptions =>
                        npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
                    .UseSnakeCaseNamingConvention());

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("DefaultConnection")!);

            return services;
        }

        private static IServiceCollection AddIdentityRoles(this IServiceCollection services)
        {
            services
                .AddIdentity<UserEntity, UserRoles>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 5;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
        public static IServiceCollection AddPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
            });

            return services;
        }

    }
}
