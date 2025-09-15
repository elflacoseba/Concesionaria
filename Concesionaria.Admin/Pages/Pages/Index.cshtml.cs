using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Concesionaria.Admin.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        { 
             ViewData["Breadcrumbs"] = new List<dynamic>
            {
                new { Nombre = "Inicio", Url = "/" }
            };
        }
    }
}
