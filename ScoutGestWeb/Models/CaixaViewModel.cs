using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace ScoutGestWeb.Models
{
    public class CaixaViewModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Não foi indicado o nome da caixa")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Não foi indicado o grupo em que a caixa anexar-se-á")]
        public string Grupo { get; set; }
        [Required(ErrorMessage = "Não foi indicado o responsável da caixa")]
        public string Responsavel { get; set; }
    }
}