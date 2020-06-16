using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
        [Required(ErrorMessage = "Não foi selecionado um tipo de documento", AllowEmptyStrings = false)]
        public string TipoDocumento { get; set; }
    }
}