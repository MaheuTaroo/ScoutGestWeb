using System.ComponentModel.DataAnnotations;
namespace ScoutGestWeb.Models
{
    public class TiposPagsViewModel
    {
        [Key]
        [StringLength(2)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sem este campo preenchido, não poderá referenciar este tipo de pagamento")]
        public string IDPagamento { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Não foi inserido o nome do pagamento")]
        public string Pagamento { get; set; }
    }
}