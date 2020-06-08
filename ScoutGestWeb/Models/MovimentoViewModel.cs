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
        [Required]
        public int IDCaixa { get; set; }
        [ForeignKey("IDDocumento")]
        [StringLength(2)]
        [Required]
        public string IDDocumento { get; set; }
        [Required]
        public DateTime DataHora { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [StringLength(65535)]
        [Required]
        public string Descricao { get; set; }
    }
}
