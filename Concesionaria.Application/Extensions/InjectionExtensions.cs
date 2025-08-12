using Concesionaria.Application.Interfaces;
using Concesionaria.Application.Mappers;
using Concesionaria.Application.Services;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Concesionaria.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // Registrar Mapster
            services.AddMapster();
            ConsultaContactoMappingConfig.RegisterMappings();

            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IConsultaContactoService, ConsultaContactoService>();

            return services;
        }
    }
}
