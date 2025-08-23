using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Concesionaria.Admin.Pages.ConsultaContacto
{
    public class ConsultasContactoModel : PageModel
    {
        public IEnumerable<ConsultaContactoDTO>? ConsultasContacto { get; set; }

        public async Task OnGetAsync()
        {
            using var client = new HttpClient();
            // Cambia la URL por la de tu API real
            var apiUrl = $"https://localhost:7062/api/ConsultasContacto";
            ConsultasContacto = await client.GetFromJsonAsync<IEnumerable<ConsultaContactoDTO>>(apiUrl);
        }
    }
}
