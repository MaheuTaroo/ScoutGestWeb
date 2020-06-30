using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Collections;

namespace ScoutGestWeb.Models
{
    //model right here
    public class InserirEscuteiroViewModel
    {
        [Key]
        [Required(ErrorMessage = "Não foi introduzido um número de escuteiro", AllowEmptyStrings = false)]
        public int ID { get; set; }
        [Required(ErrorMessage = "Por favor, insira o nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Por favor, insira o totem")]
        public string Totem { get; set; }
        public Cargos Guia { get; set; } = new Cargos("Guia");
        public Cargos Animador { get; set; } = new Cargos("Animador");
        public Cargos Cozinheiro { get; set; } = new Cargos("Cozinheiro");
        public Cargos GuardaMaterial { get; set; } = new Cargos("Guarda-material");
        public Cargos Secretario { get; set; } = new Cargos("Secretário");
        public Cargos Tesoureiro { get; set; } = new Cargos("Tesoureiro");
        public Cargos RelPub { get; set; } = new Cargos("Relações públicas");
        public Cargos Socorrista { get; set; } = new Cargos("Socorrista");
        public Cargos GuiaRegiao { get; set; } = new Cargos("Guia de região");
        public Cargos SubGuia { get; set; } = new Cargos("Sub-guia");
        public Cargos Chefe { get; set; } = new Cargos("Chefe");
        public string Cargos { get; set; }
        [Required(ErrorMessage = "Por favor, insira o telemóvel")]
        [StringLength(int.MaxValue, MinimumLength = 9, ErrorMessage = "Telemóvel incompleto; por favor, insira 9 dígitos")]
        public string NumTelefone { get; set; }
        [Required(ErrorMessage = "Não foi introduzido um grupo para o escuteiro", AllowEmptyStrings = false)]
        public string Grupo { get; set; }
        [Required(ErrorMessage = "Por favor, insira a morada")]
        [StringLength(50)]
        public string Morada { get; set; }
        [Required(ErrorMessage = "Por favor, insira a cidade")]
        [StringLength(50)]
        public string Morada2 { get; set; }
        [Required(ErrorMessage = "Por favor, insira o código-postal")]
        [RegularExpression("\\b[0-9]{4}-[0-9]{3}\\b", ErrorMessage = "Código-postal com formato errado. Por favor, insira o código-postal no formato \"NNNN-NNN\", em que cada \'N\' corresponde a um dígito entre 0 e 9")]
        [StringLength(8)]
        public string CodPostal { get; set; }
        [Required(ErrorMessage = "Por favor, insira a localidade")]
        [StringLength(100)]
        public string Localidade { get; set; }
        [Required(ErrorMessage = "Por favor, insira o grupo sanguíneo")]
        [StringLength(2)]
        public string GrupoSanguineo { get; set; }
        [StringLength(65535)]
        public string Alergias { get; set; }
        [StringLength(65535)]
        public string Medicacao { get; set; }
        [StringLength(65535)]
        public string Problemas { get; set; }
        [StringLength(255)]
        public string Observacoes { get; set; }
        [Required(ErrorMessage = "Por favor, selecione a secção")]
        public string Seccao { get; set; }
        [Required(ErrorMessage = "Por favor, selecione o estado")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "Por favor, introduza a idade")]
        public int Idade { get; set; }
        public IFormFile FotoUp { get; set; }
        public string FotoDown { get; set; }
    }
    public class Cargos
    {
        public Cargos(string cargo)
        {
            Cargo = cargo;
            Selecionado = false;
        }
        public Cargos(string cargo, bool selecionado) : this(cargo) => Selecionado = selecionado;
        public string Cargo { get; set; }
        public bool Selecionado { get; set; }
    }
}