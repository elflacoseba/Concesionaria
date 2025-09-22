using System.Text.Json;

namespace Concesionaria.Admin.Helpers
{
    public static class ConsultaContactoHelper
    {
        public static async Task<int> ObtenerCantidadNoLeidasAsync(HttpClient httpClient, string apiBaseUrl)
        {
            var response = await httpClient.GetAsync($"{apiBaseUrl}ConsultasContacto/filtrar?noLeida=true");
            //response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                return 0; // O maneja el error según tus necesidades
            }

            var json = await response.Content.ReadAsStringAsync();
            var consultas = JsonSerializer.Deserialize<List<object>>(json);
            return consultas?.Count ?? 0;
        }
    }
}