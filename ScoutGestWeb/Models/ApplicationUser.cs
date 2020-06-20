using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ScoutGestWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int IDGrupo { get; set; }
        public int Seccao { get; set; }
    }
}