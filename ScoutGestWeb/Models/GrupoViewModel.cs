﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ScoutGestWeb.Models
{
    public class GrupoViewModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Nome do grupo em falta")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Sigla do grupo em falta")]
        public string Sigla { get; set; }
        public IFormFile FotoUp { get; set; }
        public string FotoDown { get; set; }
        [Required(ErrorMessage = "Secção do grupo em falta")]
        public string Seccao { get; set; }
        public string Particips { get; set; }
    }
}
