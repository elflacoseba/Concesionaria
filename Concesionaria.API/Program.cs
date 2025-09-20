using Concesionaria.API.Application.Interfaces;
using Concesionaria.API.Application.Mappers;
using Concesionaria.API.Application.Services;
using Concesionaria.API.Data;
using Concesionaria.API.Data.Repositories;
using Concesionaria.API.Middlewares;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Concesionaria.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            // Registrar Mapster
            builder.Services.AddMapster();
            ConsultaContactoMappingConfig.RegisterMappings();

            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Services.AddScoped<IConsultaContactoService, ConsultaContactoService>();
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionDB")));
            builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddSwaggerGen(opciones =>
            {
                opciones.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Concesionaria API",
                    Version = "v1",
                    Description = "Esta es una API para gestionar una concesionaria de autos.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Sebastián Enrique Serrisuela",
                        Email = "sebaserri@gmail.com",
                        Url = new Uri("https://elflacoseba.dev"),                        
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseExceptionMiddleware();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
