using Concesionaria.API.Application.Interfaces;
using Concesionaria.API.Application.Mappers;
using Concesionaria.API.Application.Services;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Concesionaria.API.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // Registrar Mapster
            services.AddMapster();
            ConsultaContactoMappingConfig.RegisterMappings();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IConsultaContactoService, ConsultaContactoService>();

            return services;
        }
    }
}
