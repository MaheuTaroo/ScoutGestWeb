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
            List<int> seccoes = new List<int>();
            List<GrupoViewModel> gvm = new List<GrupoViewModel>();
            using (MySqlCommand cmd = new MySqlCommand("select * from grupos", UserData.UserData.con))
            {
                if (UserData.UserData.con.State == ConnectionState.Closed) UserData.UserData.con.Open();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        gvm.Add(new GrupoViewModel()
                        {
                            ID = int.Parse(dr["IDGrupo"].ToString()),
                            Nome = dr["Nome"].ToString(),
                            Sigla = dr["Sigla"].ToString(),
                            Pseudonimo = dr["Pseudonimo"].ToString()
                        });
                        seccoes.Add(int.Parse(dr["Seccao"].ToString()));
                    }
                }
                cmd.CommandText = "select Nome from seccoes where IDSeccao = @id";
                for (int i = 0; i < seccoes.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@id", seccoes[i]);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) gvm[i].Seccao = dr["Nome"].ToString();
                    }
                    cmd.Parameters.Clear();
                }
            }
            return await Task.Run(() => View(gvm));
        }
        public async Task<IActionResult> NovoGrupo()
        {
            return await Task.Run(() => !User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : (IActionResult)View());
        }
        [HttpPost]
        public async Task<IActionResult> NovoGrupo(GrupoViewModel gvm)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (ModelState.IsValid)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into grupos(Nome, Sigla, Foto, Seccao, Pseudonimo) values (@nome, @sigla, @foto, @seccao, @pseudonimo"))
                {
                    cmd.Parameters.AddWithValue("@nome", gvm.Nome);
                    cmd.Parameters.AddWithValue("@sigla", gvm.Sigla);
                    using (MemoryStream ms = new MemoryStream())
                    using (Image i = Image.Load(gvm.Foto.OpenReadStream()))
                    {
                        i.SaveAsPng(ms);
                        cmd.Parameters.AddWithValue("@foto", ms.ToArray());
                    }
                    using (MySqlCommand cmd2 = new MySqlCommand("select IDSeccao from seccoes where Nome = @seccao", UserData.UserData.con))
                    {
                        cmd2.Parameters.AddWithValue("@seccao", gvm.Seccao);
                        using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync()) cmd.Parameters.AddWithValue("@seccao", dr["IDSeccao"].ToString());
                        }
                    }
                }
            }
            return await Task.Run(() => View(gvm));
        }
    }
}