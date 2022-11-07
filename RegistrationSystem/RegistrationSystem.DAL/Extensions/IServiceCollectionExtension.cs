using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.DAL.Data;
using RegistrationSystem.DAL.Repositories;

namespace RegistrationSystem.DAL.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDbServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository,PersonRepository>();

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
