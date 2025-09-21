using Concesionaria.API.Middlewares;
using Concesionaria.Application.Extensions;
using Concesionaria.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Concesionaria.API.Data;
using Microsoft.EntityFrameworkCore;

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

            // Add Identity with its own context
            builder.Services.AddDbContext<ApiIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionDB")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApiIdentityDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {                
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            });

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
