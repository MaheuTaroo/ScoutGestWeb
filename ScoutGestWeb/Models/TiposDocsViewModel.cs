using System.ComponentModel.DataAnnotations;
namespace ScoutGestWeb.Models
{
    public class TiposDocsViewModel
    {
        [Key]
        [Required(ErrorMessage = "Sem este campo preenchido, não poderá referenciar este documento", AllowEmptyStrings = false)]
        [MinLength(2)]
        [MaxLength(2)]
        public string IDDocumento { get; set; }
        [Required(ErrorMessage = "Campo de descrição necessário", AllowEmptyStrings = false)]
        public string Descricao { get; set; }
    }
}