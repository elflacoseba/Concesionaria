using Concesionaria.Application.Interfaces;
using Concesionaria.Infrastructure.Persistence.Context;
using Concesionaria.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Concesionaria.Infrastructure.Extensions
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
