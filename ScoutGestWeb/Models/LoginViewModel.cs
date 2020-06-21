using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace ScoutGestWeb.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Não foi introduzido o username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Não foi introduzida a password")]
        public string Password { get; set; }
    }
}