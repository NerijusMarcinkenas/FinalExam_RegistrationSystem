using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using RegistrationSystem.Core.Common;
using RegistrationSystem.Core.Enums;
using RegistrationSystem.Core.IdentityServices;
using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.Core.Services;
using System.Text;


namespace RegistrationSystem.Core.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IIdentityJwtTokenService,IdentityJwtTokenService>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<IPersonService, PersonService>();

            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("User", policy =>
                {
                    policy.RequireRole(nameof(Roles.Admin), nameof(Roles.User));
                });

                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireRole(nameof(Roles.Admin));
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options => {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = configuration["Jwt:Issuer"],
                      ValidAudience = configuration["Jwt:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                  };                  
              });
            return services;
        }
    }
}
