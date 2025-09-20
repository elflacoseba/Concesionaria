using Concesionaria.API.Application.Interfaces;
using Concesionaria.API.Infrastructure.Persistence.Context;
using Concesionaria.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Concesionaria.API.Infrastructure.Extensions
{
    public static class InjectionExtension
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {           

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionDB")));

            services.AddScoped<DbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
         
            return services;
        }      
    }
}
