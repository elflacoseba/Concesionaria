using Concesionaria.Application.Interfaces;
using Concesionaria.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Concesionaria.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {

            services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
           services.AddScoped<IConsultaContactoService, ConsultaContactoService>();

            return services;
        }
    }
}
