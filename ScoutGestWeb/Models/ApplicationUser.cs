using Microsoft.AspNetCore.Identity;

namespace ScoutGestWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int IDGrupo { get; set; }
        public string Seccao { get; set; }
    }
}