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
        public List<Cargos> _cargo = new List<Cargos>() { new Cargos("Guia"), new Cargos("Animador"), new Cargos("Cozinheiro"), new Cargos("Guarda-material"), new Cargos("Secretário"), new Cargos("Tesoureiro", false), new Cargos("Relações públicas"), new Cargos("Socorrista"), new Cargos("Guia de região"), new Cargos("Sub-guia"), new Cargos("Chefe") };
        /*public List<Cargos> Cargo
        {
            get
            {
                if (_cargo == null)
                {
                    _cargo = new List<Cargos>() { new Cargos() { Cargo = "Guia", Selecionado = false }, new Cargos() { Cargo = "Animador", Selecionado = false }, new Cargos() { Cargo = "Cozinheiro", Selecionado = false }, new Cargos() { Cargo = "Guarda-material", Selecionado = false }, new Cargos() { Cargo = "Secretário", Selecionado = false }, new Cargos() { Cargo = "Tesoureiro", Selecionado = false }, new Cargos() { Cargo = "Relações públicas", Selecionado = false }, new Cargos() { Cargo = "Socorrista", Selecionado = false }, new Cargos() { Cargo = "Guia de região", Selecionado = false }, new Cargos() { Cargo = "Sub-guia", Selecionado = false }, new Cargos() { Cargo = "Chefe", Selecionado = false } };
                }
                return _cargo;
            }
            set
            {
                _cargo = value;
            }
        }*/
        //public Cargos[] cargos = new Cargos[11] { new Cargos() { Cargo = "Guia", Selecionado = false }, new Cargos() { Cargo = "Animador", Selecionado = false }, new Cargos() { Cargo = "Cozinheiro", Selecionado = false }, new Cargos() { Cargo = "Guarda-material", Selecionado = false }, new Cargos() { Cargo = "Secretário", Selecionado = false }, new Cargos() { Cargo = "Tesoureiro", Selecionado = false }, new Cargos() { Cargo = "Relações públicas", Selecionado = false }, new Cargos() { Cargo = "Socorrista", Selecionado = false }, new Cargos() { Cargo = "Guia de região", Selecionado = false }, new Cargos() { Cargo = "Sub-guia", Selecionado = false }, new Cargos() { Cargo = "Chefe", Selecionado = false } };
        [Key]
        [Required(ErrorMessage = "Não foi introduzido um número de escuteiro", AllowEmptyStrings = false)]
        public int ID { get; set; }
        [Required(ErrorMessage = "Por favor, insira o nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Por favor, insira o totem")]
        public string Totem { get; set; }
        /*[Required(ErrorMessage = "Por favor, insira o(s) cargo(s)")]
        [MaxLength(3, ErrorMessage = "O limite está definido para 3 cargos. Por favor, selecione apenas 3 cargos.")]
        [MinLength(1, ErrorMessage = "Por favor, insira pelo menos um cargo")]
        public bool[] cargos = new bool[11] { false, false, false, false, false, false, false, false, false, false, false };
        public List<Cargos> Cargo = new List<Cargos>();
        public bool Teste { get; set; }*/
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
        [Required(ErrorMessage = "Por favor, insira o telemóvel")]
        [StringLength(9, ErrorMessage = "Telemóvel incompleto; por favor, insira 9 dígitos")]
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
        public (bool, string) Adicionar()
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into Atividades values ()", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    return (true, "");
                }
            }
            catch (MySqlException mse)
            {
                return (false, string.Format("Error number {0}: {1}", mse.Number, mse.Message));
            }
        }
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