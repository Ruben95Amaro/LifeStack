using Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Infrastructure.Authentication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AuthenticationDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
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
                //update for RequireHttpsMetadata = true to ensure Https is required for metadeta
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });
            services.AddHttpContextAccessor();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ITokenProvider, TokenProvider>();
            return services;
        }
    }
}


