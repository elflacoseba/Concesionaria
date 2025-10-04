namespace Concesionaria.Admin.ViewModels
{
    public class SideNavViewModel
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public int ConsultasNoLeidas { get; set; } = 0;
    }
}