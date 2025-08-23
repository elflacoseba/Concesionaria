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
    }
}
