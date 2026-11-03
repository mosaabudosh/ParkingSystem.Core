using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingSystem.Domain.IRepositories;
using ParkingSystem.Infrastructure.Data.Context;
using ParkingSystem.Infrastructure.Data.Repositories;


namespace ParkingSystem.Infrastructure.Data.ServicesRegistration
{
    public static class ServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureDataService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ParkingSystemContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
