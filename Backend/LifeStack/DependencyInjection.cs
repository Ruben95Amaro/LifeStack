using Application;
using Domain;
using Infrastructure;
using Infrastructure.Authentication;
using Infrastructure.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Web.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppServicesDI(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMvc()
                .AddJWTSecurity()
                .AddApplicationDI()
                .AddDomainDI(configuration)
                .AddInfrastructureDI(configuration)
                .AuthenticationDI(configuration)
                .AuthorizationDI();

            return services;
        }

        public static IServiceCollection AddMvc(this IServiceCollection services)
        {

            services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });


            return services;
        }

        public static IServiceCollection AddJWTSecurity(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Enter your JWT Access Token",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {jwtSecurityScheme, Array.Empty<string>() }
                    });
            }
);
            return services;

        }


    }
}
