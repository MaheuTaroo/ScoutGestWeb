using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace ScoutGestWeb.Models
{
    //model right here
    public class InserirEscuteiroViewModel
    {

        public List<Cargos> _cargo = new List<Cargos>() { new Cargos() { Cargo = "Guia", Selecionado = false }, new Cargos() { Cargo = "Animador", Selecionado = false }, new Cargos() { Cargo = "Cozinheiro", Selecionado = false }, new Cargos() { Cargo = "Guarda-material", Selecionado = false }, new Cargos() { Cargo = "Secretário", Selecionado = false }, new Cargos() { Cargo = "Tesoureiro", Selecionado = false }, new Cargos() { Cargo = "Relações públicas", Selecionado = false }, new Cargos() { Cargo = "Socorrista", Selecionado = false }, new Cargos() { Cargo = "Guia de região", Selecionado = false }, new Cargos() { Cargo = "Sub-guia", Selecionado = false }, new Cargos() { Cargo = "Chefe", Selecionado = false } };
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
        public int ID { get; set; }
        [Required(ErrorMessage = "Por favor, insira o nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Por favor, insira o totem")]
        public string Totem { get; set; }
        [Required(ErrorMessage = "Por favor, insira o(s) cargo(s)")]
        [MaxLength(3, ErrorMessage = "O limite está definido para 3 cargos. Por favor, selecione apenas 3 cargos.")]
        [MinLength(1, ErrorMessage = "Por favor, insira pelo menos um cargo")]
        public List<Cargos> Cargo = new List<Cargos>();
        [Required(ErrorMessage = "Por favor, insira o telemóvel")]
        [StringLength(9, ErrorMessage = "Telemóvel incompleto; por favor, insira 9 dígitos")]
        public string NumTelefone { get; set; }
        [Required(ErrorMessage = "Por favor, insira a morada")]
        [StringLength(50)]
        public string Morada { get; set; }
        [Required(ErrorMessage = "Por favor, insira a cidade")]
        [StringLength(50)]
        public string Morada2 { get; set; }
        [Required(ErrorMessage = "Por favor, insira o código-postal")]
        [StringLength(8)]
        public string CodPostal { get; set; }
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
        public IFormFile Foto { get; set; }
        public (bool, string) Adicionar()
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into Atividades values ()", UserData.UserData.con))
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
        public string Cargo { get; set; }
        public bool Selecionado { get; set; }
    }
}