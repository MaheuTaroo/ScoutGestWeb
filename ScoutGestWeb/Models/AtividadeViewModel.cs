using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutGestWeb.Models
{
    public class AtividadeViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDAtividade { get; set; }
        [Required(ErrorMessage = "Não foi definido um nome para a atividade")]
        public string Nome { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        [ForeignKey("IDSeccao")]
        public int Seccao { get; set; }
        [Required(ErrorMessage = "Não foi atribuído um local à atividade")]
        public string Local { get; set; }
        [Required(ErrorMessage = "Não foi definida uma data de início à atividade")]
        public DateTime DataInicio { get; set; }
        [Required(ErrorMessage = "Não foi definida uma data final à atividade")]
        public DateTime DataFim { get; set; }
        [Required(ErrorMessage = "Não foi confirmada a abertura da atividade a movimentos")]
        public bool Ativa { get; set; }
    }
}
