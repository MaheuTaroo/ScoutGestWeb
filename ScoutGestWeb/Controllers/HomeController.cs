using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScoutGestWeb.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ScoutGestWeb.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => UserData.UserData.userData.Count == 0/* || Request.Cookies["User"] == null*/ ? View("Login") : View("Dashboard"));
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                using (MD5 md5 = new MD5CryptoServiceProvider())
                {
                    md5.ComputeHash(Encoding.ASCII.GetBytes(login.Password));
                    for (int i = 0; i < md5.Hash.Length; i++) sb.Append(md5.Hash[i].ToString("x2"));
                }
                IdentityUser user = await _userManager.FindByNameAsync(login.Username);
                /*var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, true, false);*/
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, sb.ToString(), false, false);
                    if (result.Succeeded) Console.WriteLine("noice");
                    else Console.WriteLine("fook");
                }
                using (MySqlCommand cmd = new MySqlCommand("select * from users where User = @user and Pass = @pass", UserData.UserData.con))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) if (UserData.UserData.con.State == ConnectionState.Closed) UserData.UserData.con.Open();
                    cmd.Parameters.AddWithValue("@user", login.Username);
                    cmd.Parameters.AddWithValue("@pass", sb.ToString());
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        if (dr.HasRows)
                        {
                            while (await dr.ReadAsync())
                            {
                                /*Response.Cookies.Append("User", dr["User"].ToString(), new Microsoft.AspNetCore.Http.CookieOptions()
                                {
                                    Expires = DateTime.MinValue
                                });*/
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
                    return await Task.Run(() => View("Dashboard"));
                }
            }
            return await Task.Run(() => View("Login"));
        }
        public async Task<IActionResult> LogOut()
        {
            if (Request.Cookies["User"] != null) Response.Cookies.Delete("User");
            UserData.UserData.userData.Clear();
            return await Task.Run(() => RedirectToAction("Index"));
        }
        public async Task<IActionResult> InfoLogout()
        {
            return await Task.Run(() => View());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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