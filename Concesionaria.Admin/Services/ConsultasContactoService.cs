using Concesionaria.Admin.Models;
using Concesionaria.Admin.Services.Interfaces;

namespace Concesionaria.Admin.Services
{
    public class ConsultasContactoService : IConsultasContactoService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl;
        private readonly HttpClient _client;

        public ConsultasContactoService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration["ApiBaseUrl"]!;
            _client = _httpClientFactory.CreateClient("ConsultasContactoApi");
        }

        public async Task<IEnumerable<ConsultaContacto>?> GetConsultasContactoAsync()
        {
            return await _client.GetFromJsonAsync<IEnumerable<ConsultaContacto>>(_apiBaseUrl + "ConsultasContacto");
        }

        public async Task<ConsultaContacto?> GetConsultaContactoByIdAsync(int id)
        {
            var result = await _client.GetAsync(_apiBaseUrl + $"ConsultasContacto/{id}");

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ConsultaContacto>();
            }

            return null;
        }

        public async Task<ConsultaContacto> CrearConsultaContactoAsync(ConsultaContactoCreacion consulta)
        {
            var response = await _client.PostAsJsonAsync(_apiBaseUrl + "ConsultasContacto", consulta);

            if (response.IsSuccessStatusCode)
            {
                var consultaCreada = await response.Content.ReadFromJsonAsync<ConsultaContacto>();
                
                if (consultaCreada is not null)
                {
                    return consultaCreada;
                }
                throw new Exception("La respuesta de la API fue nula al crear la consulta de contacto.");
            }
            throw new Exception("Error al crear la consulta de contacto");
        }

        public async Task<bool> MarcarConsultaContactoLeidaByIdAsync(int id, bool leida)
        {
            var response = await _client.PatchAsync(_apiBaseUrl + $"ConsultasContacto/{id}/leida?leida={leida.ToString()}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarConsultaContactoByIdAsync(int id)
        {            
            var response = await _client.DeleteAsync(_apiBaseUrl + $"ConsultasContacto/{id}");
            return response.IsSuccessStatusCode;
        }

    }
}
