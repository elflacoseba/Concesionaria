using Concesionaria.Admin.Services.Interfaces;
using Concesionaria.Admin.Services;

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
builder.Services.AddHttpClient("ConsultasContactoApi", client =>
{
    client.BaseAddress = new Uri(configuration["API_URL_BASE"]!);
});
builder.Services.AddScoped<IConsultasContactoService, ConsultasContactoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
