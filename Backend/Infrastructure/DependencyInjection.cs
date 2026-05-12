using Application.Abstractions.Authentication;
using Domain.Users.Interfaces;
using Infrastructure.Authentication;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructureDI(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddDatabase(configuration)
            .AddHealthChecks(configuration)
            .AddAuthenticationInternal(configuration);

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

        private static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    var secret = configuration["Jwt:Secret"];
                    if (string.IsNullOrWhiteSpace(secret))
                        throw new Exception("Jwt:Secret isn't configurated");

                    var issuer = configuration["Jwt:Issuer"];
                    if (string.IsNullOrWhiteSpace(issuer))
                        throw new Exception("Jwt:Issuer isn't configurated");

                    var audience = configuration["Jwt:Audience"];
                    if (string.IsNullOrWhiteSpace(audience))
                        throw new Exception("Jwt:Audience isn't configurated");

                    o.RequireHttpsMetadata = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!)),
                        ValidIssuer = issuer!,
                        ValidAudience = audience!,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddHttpContextAccessor();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ITokenProvider, TokenProvider>();

            return services;
        }
    }
}
