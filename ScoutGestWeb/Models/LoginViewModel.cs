using System.ComponentModel.DataAnnotations;
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