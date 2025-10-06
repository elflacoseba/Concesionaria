using Concesionaria.Admin.Services.Interfaces;
using Concesionaria.Admin.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages()
    .AddMvcOptions(options => 
    {
        options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "El campo es requerido.");
    });
builder.Services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");

// Registrar HttpClientFactory y el servicio personalizado
builder.Services.AddHttpClient("ClienteConcesionariaAPI", client =>
{
    client.BaseAddress = new Uri(configuration["API_URL_BASE"]!);
});

builder.Services.AddScoped<IConsultasContactoService, ConsultasContactoService>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();

// Configuración regional para Argentina
var cultureInfo = new CultureInfo("es-AR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Configuración de localización
var supportedCultures = new[] { cultureInfo };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(cultureInfo),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
