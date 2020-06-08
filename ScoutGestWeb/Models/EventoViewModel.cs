using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutGestWeb.Models
{
    public class EventoViewModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Não foi introduzido um nome")]
        public string Nome { get; set; }
        public byte[] Foto { get; set; }
        [Required(ErrorMessage="Não foi introduzido um local")]
        [StringLength(65535)]
        public string Local { get; set; }
        public string Seccao { get; set; }
        [Required(ErrorMessage ="Não foi adicionada uma descrição")]
        [StringLength(65535)]
        public string Descricao { get; set; }
        [Required(ErrorMessage="Não foi introduzida a data do começo")]
        public DateTime DataInicio { get; set; }
        [Required(ErrorMessage = "Não foi introduzida a data final")]
        public DateTime DataFim { get; set; }
    }
}
