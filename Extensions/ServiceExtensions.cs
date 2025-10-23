using SmartPayMobileApp_Backend.Repositories.Implementations;
using SmartPayMobileApp_Backend.Repositories.Interfaces;
using SmartPayMobileApp_Backend.Services.Implementations;
using SmartPayMobileApp_Backend.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SmartPayMobileApp_Backend.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Dapper connection factory
            services.AddScoped<IDbConnection>(sp =>
            {
                var cs = configuration.GetConnectionString("DefaultConnection");
                return new SqlConnection(cs);
            });

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
