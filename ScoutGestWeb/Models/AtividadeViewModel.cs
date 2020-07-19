using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Tema { get; set; }
        [Required]
        public string Seccao { get; set; }
        [Required(ErrorMessage = "Não foi atribuído um local à atividade")]
        public string Local { get; set; }
        [Required(ErrorMessage = "Não foi definida uma data de início à atividade")]
        public DateTime DataInicio { get; set; }
        [Required(ErrorMessage = "Não foi definida uma data final à atividade")]
        public DateTime DataFim { get; set; }
        [Required(ErrorMessage = "Não foi definido um orçamento")]
        [Range(0.00, 9999999999999999999999999999999999999999999999999999999.99, ErrorMessage = "O orçamento não se encontra dentro dos parâmetros")]
        public decimal Orcamento { get; set; }
        [Required(ErrorMessage = "Não foi confirmada a abertura da atividade a movimentos")]
        public bool Ativa { get; set; }
        [StringLength(1, ErrorMessage = "Não foram adicionados participantes à atividade")]
        public string Participantes { get; set; }
        public string RecHumanos { get; set; }
        public string RecFinanceiros { get; set; }
        public string RecMateriais { get; set; }
    }
}
