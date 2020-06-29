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

namespace ScoutGestWeb.Controllers
{
    public class GruposController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<GrupoViewModel> gvm = new List<GrupoViewModel>();
            using (MySqlCommand cmd = new MySqlCommand("select * from grupos", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();;
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        gvm.Add(new GrupoViewModel()
                        {
                            ID = int.Parse(dr["IDGrupo"].ToString()),
                            Nome = dr["Nome"].ToString(),
                            Sigla = dr["Sigla"].ToString(),
                            Seccao = dr["Seccao"].ToString(),
                            Pseudonimo = dr["Pseudonimo"].ToString()
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
            return await Task.Run(() => (IActionResult)View());
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
            }
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
                        Seccao = dr["Seccao"].ToString(),
                        Pseudonimo = dr["Pseudonimo"].ToString()
                    };
                }
            }
            return View(gvm);
        }
    }
}