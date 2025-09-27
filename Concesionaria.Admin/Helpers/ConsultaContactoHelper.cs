using System.Text.Json;

namespace Concesionaria.Admin.Helpers
{
    public static class ConsultaContactoHelper
    {
        public static async Task<int> ObtenerCantidadNoLeidasAsync(HttpClient httpClient, string apiBaseUrl, HttpContext httpContext)
        {
            var token = httpContext.Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {   
                // Redirige al login si no hay token
                httpContext.Response.Redirect("/Auth/SignIn");
                return 0;
            }

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync($"{apiBaseUrl}ConsultasContacto/filtrar?noLeida=true");
            response.EnsureSuccessStatusCode();
                
            var json = await response.Content.ReadAsStringAsync();
            var consultas = JsonSerializer.Deserialize<List<object>>(json);
            return consultas?.Count ?? 0;
        }
    }
}