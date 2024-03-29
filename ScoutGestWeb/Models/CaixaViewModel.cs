﻿using System.ComponentModel.DataAnnotations;
namespace ScoutGestWeb.Models
{
    public class CaixaViewModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Não foi indicado o nome da caixa", AllowEmptyStrings = false)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Não foi indicado o grupo ao qual caixa anexar-se-á", AllowEmptyStrings = false)]
        public string Grupo { get; set; }
        [Required(ErrorMessage = "Não foi indicado o responsável da caixa", AllowEmptyStrings = false)]
        public string Responsavel { get; set; }
        [Required(ErrorMessage = "Não foi definido um saldo inicial para esta caixa")]
        public decimal Saldo { get; set; }
    }
}