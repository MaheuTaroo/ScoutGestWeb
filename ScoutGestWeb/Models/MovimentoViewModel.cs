using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutGestWeb.Models
{
    public class MovimentoViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDMovimento { get; set; }
        [ForeignKey("IDCaixa")]
        [Required(ErrorMessage = "Não foi referida a caixa de entrada ou saída")]
        public string IDCaixa { get; set; }
        [ForeignKey("IDDocumento")]
        [StringLength(2)]
        [Required(ErrorMessage = "Não foi referido o tipo de documento para o movimento", AllowEmptyStrings = false)]
        public string IDDocumento { get; set; }
        [Required(ErrorMessage = "Não foi indicado um tipo de movimento", AllowEmptyStrings = false)]
        [ForeignKey("IDTipoMov")]
        public string TipoMovimento { get; set; }
        [Required(ErrorMessage = "Não foi indicado o utilizador que criou este movimento", AllowEmptyStrings = false)]
        public string User { get; set; } = "AdminAgr";
        [Required(ErrorMessage = "Não foi indicada uma data e hora para o movimento")]
        [DateTimeRangeAttribute.DateTimeRange("01/01/1900 00:00:00")]
        public DateTime DataHora { get; set; }
        [Required(ErrorMessage = "Não foi referido um valor para um movimento")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "Não foi indicado um tipo de pagamento", AllowEmptyStrings = false)]
        [ForeignKey("TipoPag")]
        public string TipoPagamento { get; set; }
        [StringLength(65535)]
        [Required(ErrorMessage = "Não foi indicada uma descrição do movimento", AllowEmptyStrings = false)]
        public string Descricao { get; set; }
    }
}