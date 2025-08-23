using Concesionaria.Admin.Models;
using Concesionaria.Admin.Services.Interfaces;

namespace Concesionaria.Admin.Services
{
    public class ConsultasContactoService : IConsultasContactoService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl;        

        public ConsultasContactoService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration["ApiBaseUrl"]!;            
        }

        public async Task<IEnumerable<ConsultaContacto>?> GetConsultasContactoAsync()
        {
           var _client = _httpClientFactory.CreateClient("ConsultasContactoApi");
            return await _client.GetFromJsonAsync<IEnumerable<ConsultaContacto>>(_apiBaseUrl + "ConsultasContacto");
        }

        public async Task<ConsultaContacto?> GetConsultaContactoByIdAsync(int id)
        {
            var _client = _httpClientFactory.CreateClient("ConsultasContactoApi");
            return await _client.GetFromJsonAsync<ConsultaContacto>(_apiBaseUrl + $"ConsultasContacto/{id}");
        }
    }
}
