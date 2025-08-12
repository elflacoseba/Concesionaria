using Concesionaria.Application.Extensions;
using Concesionaria.Infrastructure.Extensions;
using Concesionaria.API.Middlewares;

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
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseExceptionMiddleware();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
