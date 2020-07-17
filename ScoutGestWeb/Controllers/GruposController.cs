using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ScoutGestWeb.Models;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace ScoutGestWeb.Controllers
{
    [RequireHttps]
    public class GruposController : Controller
    {
        bool insert = true;
        private readonly UserManager<ApplicationUser> _userManager;
        public GruposController(UserManager<ApplicationUser> userManager) => _userManager = userManager;
        public async Task<IActionResult> Index(string coluna, string procura)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<GrupoViewModel> gvm = new List<GrupoViewModel>();
            if (TempData["msg"] != null) TempData["msgKeep"] = TempData["msg"];
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
        #region Novo grupo
        [HttpGet]
        public async Task<IActionResult> NovoGrupo()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            insert = true;
            ViewBag.seccoes = new List<string>(new string[6] { "Lobitos", "Exploradores", "Pioneiros", "Caminheiros", "Dirigentes", "Agrupamento" });
            return await Task.Run(() => View());
        }
        public async Task<IActionResult> NovoGrupo(object model)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            TempData["insert"] = insert;
            return await Task.Run(() => View("NovoGrupo", model));
        }
        [HttpPost]
        public async Task<IActionResult> NovoGrupo(GrupoViewModel gvm, int? id = null)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (ModelState.IsValid)
            {
                using (MySqlCommand cmd = new MySqlCommand(insert ? "insert into grupos(Nome, Sigla, Foto, Seccao, Pseudonimo) values (@nome, @sigla, @foto, @seccao, @pseudonimo)" : "update grupos set Nome = @nome, Sigla = @sigla, Foto =  @foto, Seccao = @seccao, Pseudonimo = @pseudonimo where IDGrupo = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                    if (id != null) cmd.Parameters.AddWithValue("@id", id);
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
        #endregion
        public async Task<IActionResult> Detalhes(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            try
            {
                GrupoViewModel gvm = null;
                using (MySqlCommand cmd = new MySqlCommand("select * from grupos where IDGrupo = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Prepare();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        if (!dr.HasRows) throw new Exception($"não foi encontrado nenhum registo com o ID \"{id}\"");
                        else
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
                    }
                    cmd.CommandText = "select Totem from escuteiros where Grupo = @id";
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) gvm.Particips += dr["Totem"].ToString() + "<br />";
                    }
                    gvm.Particips = gvm.Particips == "" ? "nenhum participante" : gvm.Particips.Substring(0, gvm.Particips.LastIndexOf("<br />"));
                }
                return View(gvm);
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a apresentação dos detalhes do registo: " + e.Message;
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        public async Task<IActionResult> Editar(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            try
            {
                ViewBag.seccoes = new List<string>(new string[6] { "Lobitos", "Exploradores", "Pioneiros", "Caminheiros", "Dirigentes", "Agrupamento" });
                GrupoViewModel gvm = new GrupoViewModel();
                using (MySqlCommand cmd = new MySqlCommand("select * from grupos where IDGrupo = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        if (!dr.HasRows) throw new Exception($"não foi encontrado nenhum registo com o ID \"{id}\"");
                        else
                        {
                            while (await dr.ReadAsync())
                            {
                                gvm.Nome = dr["Nome"].ToString();
                                gvm.Sigla = dr["Sigla"].ToString();
                                gvm.Seccao = dr["Seccao"].ToString();
                                gvm.FotoDown = "data:image/png;base64," + Convert.ToBase64String((byte[])dr["Foto"]);
                            }
                        }
                    }
                    return await Task.Run(() => NovoGrupo((object)gvm));
                }
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a edição dos detalhes do registo: " + e.Message;
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        #region Eliminar
        public async Task<IActionResult> Eliminar(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            try
            {
                GrupoViewModel gvm = new GrupoViewModel();
                using (MySqlCommand cmd = new MySqlCommand("select * from grupos where IDGrupo = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        if (!dr.HasRows) throw new Exception($"não foi encontrado nenhum registo com o ID \"{id}\"");
                        else
                        {
                            while (await dr.ReadAsync())
                            {
                                gvm.ID = int.Parse(dr["IDGrupo"].ToString());
                                gvm.Nome = dr["Nome"].ToString();
                                gvm.Sigla = dr["Sigla"].ToString();
                                gvm.FotoDown = "data:image/png;base64," + Convert.ToBase64String((byte[])dr["Foto"]);
                            }
                        }
                    }
                    cmd.CommandText = "select Totem from escuteiros where Grupo = @id";
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            while (await dr.ReadAsync()) gvm.Particips += dr["Totem"].ToString() + "<br />";
                        }
                    }
                    gvm.Particips = gvm.Particips == "" ? "nenhum participante" : gvm.Particips.Substring(0, gvm.Particips.LastIndexOf("<br />"));
                    return await Task.Run(() => View(gvm));
                }
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a eliminação do registo: " + e.Message;
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        [HttpPost]
        public async Task<IActionResult> EliminarPost(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("delete from grupos where IDGrupo = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.PrepareAsync();
                    int i = await cmd.ExecuteNonQueryAsync();
                    if (i == 0) throw new Exception($"não foi encontrado nenhum registo com o ID \"{id}\"");
                }
            }
            catch (MySqlException mse)
            {
                if (mse.Number == 1451) TempData["msg"] = "este grupo tem dados associados a si mesmo, como escuteiros anexados a este, atividades em que este participou, ou caixas associadas ao mesmo. Procure esses dados e remova as ligações a este grupo, de modo a eliminá-lo com segurança.";
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a eliminação do registo: " + e.Message;
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        #endregion
    }
}