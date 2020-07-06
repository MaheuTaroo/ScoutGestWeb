using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ScoutGestWeb.Models;
using SixLabors.ImageSharp;
using Microsoft.AspNetCore.Identity;

namespace ScoutGestWeb.Controllers
{
    public class GruposController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public GruposController(UserManager<ApplicationUser> userManager) => _userManager = userManager;
        public async Task<IActionResult> Index(string coluna, string procura)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<GrupoViewModel> gvm = new List<GrupoViewModel>();
            using (MySqlCommand cmd = new MySqlCommand("select * from grupos where IDGrupo > 0;", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                if (!(string.IsNullOrEmpty(coluna) && string.IsNullOrEmpty(procura)))
                {
                    cmd.CommandText = cmd.CommandText.Replace(";", " and pesquisa;");
                    switch (coluna)
                    {
                        case "Nome":
                        case "Sigla":
                            cmd.CommandText = cmd.CommandText.Replace("pesquisa", coluna + " like '%" + procura + "%'");
                            break;
                        case "Secção":
                            cmd.CommandText = cmd.CommandText.Replace("pesquisa", "Seccao like '%" + procura + "%'");
                            break;
                    }
                }
                if (User.IsInRole("Equipa de Animação"))
                {
                    cmd.CommandText += " and Seccao = @seccao";
                    cmd.Parameters.AddWithValue("@seccao", (await _userManager.GetUserAsync(User)).Seccao);
                }
                await cmd.PrepareAsync();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        gvm.Add(new GrupoViewModel()
                        {
                            ID = int.Parse(dr["IDGrupo"].ToString()),
                            Nome = dr["Nome"].ToString(),
                            Sigla = dr["Sigla"].ToString(),
                            Seccao = dr["Seccao"].ToString()
                        });
                    }
                }
            }
            return await Task.Run(() => View(gvm));
        }
        public async Task<IActionResult> NovoGrupo()
        {
            if (!User.Identity.IsAuthenticated) RedirectToAction("Index", "Home");
            ViewBag.seccoes = new List<string>(new string[6] { "Lobitos", "Exploradores", "Pioneiros", "Caminheiros", "Dirigentes", "Agrupamento" });
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<IActionResult> NovoGrupo(GrupoViewModel gvm)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (ModelState.IsValid)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into grupos(Nome, Sigla, Foto, Seccao, Pseudonimo) values (@nome, @sigla, @foto, @seccao, @pseudonimo", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
                    cmd.Parameters.AddWithValue("@nome", gvm.Nome);
                    cmd.Parameters.AddWithValue("@sigla", gvm.Sigla);
                    cmd.Parameters.AddWithValue("@seccao", gvm.Seccao);
                    using (MemoryStream ms = new MemoryStream())
                    using (Image i = Image.Load(gvm.FotoUp.OpenReadStream()))
                    {
                        i.SaveAsPng(ms);
                        cmd.Parameters.AddWithValue("@foto", ms.ToArray());
                    }
                }
                return await Task.Run(() => RedirectToAction("Index"));
            }
            ViewBag.seccoes = new List<string>(new string[6] { "Lobitos", "Exploradores", "Pioneiros", "Caminheiros", "Dirigentes", "Agrupamento" });
            return await Task.Run(() => View(gvm));
        }
        public async Task<IActionResult> Detalhes(int id)
        {
            if (!User.Identity.IsAuthenticated) RedirectToAction("Index", "Home");
            GrupoViewModel gvm = null;
            using (MySqlCommand cmd = new MySqlCommand("select * from grupos where IDGrupo = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) gvm = new GrupoViewModel()
                    {
                        ID = int.Parse(dr["IDGrupo"].ToString()),
                        Nome = dr["Nome"].ToString(),
                        Sigla = dr["Sigla"].ToString(),
                        FotoDown = "data:image/png;base64," + Convert.ToBase64String((byte[])dr["Foto"]),
                        Seccao = dr["Seccao"].ToString()
                    };
                }
                cmd.CommandText = "select Totem from escuteiros where Grupo = @id";
                await cmd.PrepareAsync();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) gvm.Partips += dr["Totem"].ToString() + "<br />";
                }
                gvm.Partips = gvm.Partips.Substring(0, gvm.Partips.LastIndexOf("<br />"));
            }
            return View(gvm);
        }
        public async Task<IActionResult> Editar(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            ViewBag.seccoes = new List<string>(new string[6] { "Lobitos", "Exploradores", "Pioneiros", "Caminheiros", "Dirigentes", "Agrupamento" });
            GrupoViewModel gvm = new GrupoViewModel();
            using (MySqlCommand cmd = new MySqlCommand("select * from grupos where IDGrupo = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        gvm.Nome = dr["Nome"].ToString();
                        gvm.Sigla = dr["Sigla"].ToString();
                        gvm.Seccao = dr["Seccao"].ToString();
                    }
                }
                return await Task.Run(() => View("NovoGrupo", gvm));
            }
        }
    }
}