﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        [Required(ErrorMessage = "Pseudónimo do grupo em falta\r\nExemplo de um pseudónimo: Eq. Santa Clara de Assis (nome) => Santa Clara (pseudónimo)")]
        public string Pseudonimo { get; set; }
    }
}
