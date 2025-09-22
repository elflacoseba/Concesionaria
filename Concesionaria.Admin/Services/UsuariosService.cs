using Concesionaria.Admin.DTOs;
using Concesionaria.Admin.Services.Interfaces;

namespace Concesionaria.Admin.Services
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl;
        private readonly HttpClient _client;

        public UsuariosService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration["ApiBaseUrl"]!;
            _client = _httpClientFactory.CreateClient("ClienteConcesionariaAPI");
        }

        public async Task<RespuestaAutenticacionDto> LoginAsync(CredencialesUsuarioDto credencialesUsuarioDto)
        {
            var response = await _client.PostAsJsonAsync(_apiBaseUrl + "Usuarios/Login", credencialesUsuarioDto);
            
            if (!response.IsSuccessStatusCode)
                return null!;

            var resultado = await response.Content.ReadFromJsonAsync<RespuestaAutenticacionDto>();
            return resultado!;

        }
    }
}
