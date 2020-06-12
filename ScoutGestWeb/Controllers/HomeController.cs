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

namespace ScoutGestWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return UserData.UserData.userData.Count == 0 ? View("Login") : View("Dashboard");
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel login)
        {
            List<EventoViewModel> evm = new List<EventoViewModel>();
            if (string.IsNullOrEmpty(login.Password)) return View("Login");
            else
            {
                StringBuilder sb = new StringBuilder();
                using (MD5 md5 = new MD5CryptoServiceProvider())
                {
                    md5.ComputeHash(Encoding.ASCII.GetBytes(login.Password));
                    for (int i = 0; i < md5.Hash.Length; i++) sb.Append(md5.Hash[i].ToString("x2"));
                }
                using (MySqlCommand cmd = new MySqlCommand("select * from users where User = @user and Pass = @pass", UserData.UserData.con))
                {
                    //desliguei o server rapidamente pq tive um prob c ele. ja reiniciei
                    //oki 
                    //vou ver uma cena
                    if (cmd.Connection.State == ConnectionState.Closed) if (UserData.UserData.con.State == ConnectionState.Closed) UserData.UserData.con.Open();
                    cmd.Parameters.AddWithValue("@user", login.Username);
                    cmd.Parameters.AddWithValue("@pass", sb.ToString());
                    cmd.Prepare();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            //fizeste login alguma vez?
                            //sim
                            //n facas qd eu fzr pfv, enquanto n pensar noutro metodo p o fzr
                            //oki sorry
                            while (dr.Read()) for (int i = 0; i < dr.FieldCount; i++) UserData.UserData.userData.Add(dr.GetSchemaTable().Rows[i].Field<string>("ColumnName"), dr[i].ToString());
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
                        cmd.CommandText = "select * from eventos";
                        //ja conseguiste a imagem?
                        //q img?
                        //a imagem para um evento qlq ou referes que seja eu que arranje?
                        //eu ainda tou na fase d testes, ainda n arranjei nd
                        //okin entao eu vou arranjar uma so para ver se o cod funciona
                        //yeah forget it tens de seer yu a meter a imagen. 

                        cmd.Prepare();
                        //n te ias embora?
                        //so estava a espera da tua resposta, pq eu nao me ia embora se ainda precissases de ajuda
                        //eu vou tar td o dia a trabalhar nisto, se entretanto quiseres vir vem
                        //oki then
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                evm.Add(new EventoViewModel()
                                {
                                    ID = int.Parse(dr["IDEvento"].ToString()),
                                    Nome = dr["Nome"].ToString(),
                                    Descricao = dr["Descricao"].ToString(),
                                    Seccao = dr["Seccao"].ToString(),
                                    Local = dr["Local"].ToString(),
                                    DataInicio = DateTime.Parse(dr["DataInicio"].ToString()),
                                    DataFim = DateTime.Parse(dr["DataFim"].ToString())
                                });
                                /*hey acho que vou andado, vou passar para o relatorio, antes de ir precisas que eu faça
                                mais alguma cena?*/
                            }
                        }
                        return View("Dashboard", evm);
                    }
                }
                return View("Login");
            }
        }
        [Route("/Home/Escuteiros", Name = "Escuteiros")]
        public IActionResult Escuteiros()
        {
            return RedirectToAction("Index", "InserirEscuteiro");
        }
        public IActionResult LogOut()
        {
            UserData.UserData.userData.Clear();
            return RedirectToAction("Index");
        }
        public IActionResult InfoLogout()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
