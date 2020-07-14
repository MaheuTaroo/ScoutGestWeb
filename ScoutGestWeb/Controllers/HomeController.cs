﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScoutGestWeb.Models;
using System.Threading.Tasks;

namespace ScoutGestWeb.Controllers
{
    [RequireHttps]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => !User.Identity.IsAuthenticated ? View("Login") : View("Dashboard")); ;
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByNameAsync(login.Username);
                if (user != null)
                {
                    if (user.LockoutEnabled)
                    {
                        ModelState.AddModelError("", "Não é possível iniciar sessão, pois esta conta está trancada");
                        return await Task.Run(() => Index());
                    }
                    var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);
                    if (result.Succeeded) return await Task.Run(() => View("Dashboard"));
                }
                ModelState.AddModelError("", "Credenciais incorretas");
                return await Task.Run(() => Index());
            }
            return await Task.Run(() => View("Login", login));
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return await Task.Run(() => RedirectToAction("Index"));
        }
        public async Task<IActionResult> InfoLogout()
        {
            return await Task.Run(() => !User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : (IActionResult)View());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            ViewBag.error = statusCode;
            return View();
        }
    }
}

//desliguei o server rapidamente pq tive um prob c ele. ja reiniciei
//oki 
//vou ver uma cena
//fizeste login alguma vez?
//sim
//n facas qd eu fzr pfv, enquanto n pensar noutro metodo p o fzr
//oki sorry
//ja conseguiste a imagem?
//q img?
//a imagem para um evento qlq ou referes que seja eu que arranje?
//eu ainda tou na fase d testes, ainda n arranjei nd
//okin entao eu vou arranjar uma so para ver se o cod funciona
//yeah forget it tens de seer yu a meter a imagen. 
//n te ias embora?
//so estava a espera da tua resposta, pq eu nao me ia embora se ainda precissases de ajuda
//eu vou tar td o dia a trabalhar nisto, se entretanto quiseres vir vem
//oki then
/*using (MySqlCommand cmd = new MySqlCommand("select * from users where User = @user and Pass = @pass", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
{
    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
    cmd.Parameters.AddWithValue("@user", login.Username);
    cmd.Parameters.AddWithValue("@pass", sb.ToString());
    await cmd.PrepareAsync();
    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
    {
        if (dr.HasRows)
        {
            while (await dr.ReadAsync())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    UserData.UserData.userData.Add(dr.GetSchemaTable().Rows[i].Field<string>("ColumnName"), dr[i].ToString());
                }
            }
        }
    }
    if (UserData.UserData.userData.Count > 0)
    {
        cmd.CommandText = "select Nome from grupos where IDGrupo = @id;";
        cmd.Parameters.AddWithValue("@id", UserData.UserData.userData["IDGrupo"]);
        cmd.Prepare();
        using (MySqlDataReader dr = cmd.ExecuteReader())
        {
            //my login is broke for now
            while (dr.Read()) UserData.UserData.userData.Add("Nome", dr["Nome"]);
        }
    }
}*/
