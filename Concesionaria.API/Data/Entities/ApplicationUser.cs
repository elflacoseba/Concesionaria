using Microsoft.AspNetCore.Identity;

namespace Concesionaria.API.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
    }
}