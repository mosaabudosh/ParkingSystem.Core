
using Microsoft.Extensions.DependencyInjection;
using ParkingSystem.Application.Interfaces;
using ParkingSystem.Application.Security;
using ParkingSystem.Application.Services;
using System.Reflection;

namespace ParkingSystem.Application.ServicesRegistration
{
    public static class ServicesRegistration
    {
        public static IServiceCollection ConfigureParkingSystemService(this IServiceCollection services, ServiceProvider serviceProvider)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<ITokenHandler, TokenHandler>();
            services.AddSignalR();
            Assembly assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
            return services;
        }
    }
}
