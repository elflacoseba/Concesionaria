using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Filters;

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

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                context.Result = RedirectToPage("/Auth/SignIn");
            }
            // Si quieres validar la expiración del token, puedes decodificarlo y verificar el claim "exp"
            base.OnPageHandlerExecuting(context);
        }
    }
}
