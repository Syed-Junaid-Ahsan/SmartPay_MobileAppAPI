using Microsoft.EntityFrameworkCore;
using SmartPayMobileApp_Backend.Data;
using SmartPayMobileApp_Backend.Repositories.Implementations;
using SmartPayMobileApp_Backend.Repositories.Interfaces;
using SmartPayMobileApp_Backend.Services.Implementations;
using SmartPayMobileApp_Backend.Services.Interfaces;

namespace SmartPayMobileApp_Backend.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IConsumerNumberRepository, ConsumerNumberRepository>();

            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IConsumerNumberService, ConsumerNumberService>();

            return services;
        }
    }
}
