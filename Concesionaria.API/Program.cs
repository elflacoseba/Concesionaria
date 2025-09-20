using Concesionaria.API.Middlewares;
using Concesionaria.API.Application.Extensions;
using Concesionaria.API.Infrastructure.Extensions;

namespace Concesionaria.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddApplicationLayer();
            builder.Services.AddInfrastructureLayer(configuration);

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
