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
            var client = _httpClientFactory.CreateClient("ConsultasContactoApi");
            return await client.GetFromJsonAsync<IEnumerable<ConsultaContacto>>(_apiBaseUrl + "ConsultasContacto");
        }
    }
}
