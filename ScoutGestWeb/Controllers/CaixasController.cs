﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ScoutGestWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ScoutGestWeb.Controllers
{
    [RequireHttps]
    public class CaixasController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly List<CaixaViewModel> cvm = new List<CaixaViewModel>();
        private readonly List<int> grupos = new List<int>(), responsaveis = new List<int>();
        public CaixasController(UserManager<ApplicationUser> userManager) => _userManager = userManager;
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (TempData["msg"] != null) TempData["msgKeep"] = TempData["msg"];
            using (MySqlCommand cmd = new MySqlCommand("select * from caixas", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                if (User.IsInRole("Comum"))
                {
                    cmd.CommandText += " where Grupo = @id";
                    cmd.Parameters.AddWithValue("@id", (await _userManager.GetUserAsync(User)).IDGrupo);
                }
                else if (User.IsInRole("Equipa de Animação"))
                {
                    cmd.CommandText += " where Grupo in (select IDGrupo from grupos where Seccao = @seccao)";
                    cmd.Parameters.AddWithValue("@seccao", (await _userManager.GetUserAsync(User)).Seccao);
                }
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        cvm.Add(new CaixaViewModel()
                        {
                            ID = int.Parse(dr["IDCaixa"].ToString()),
                            Nome = dr["Nome"].ToString(),
                            Responsavel = dr["Responsavel"].ToString() + " - ",
                            Saldo = decimal.Parse(dr["Saldo"].ToString())
                        });
                        grupos.Add(int.Parse(dr["Grupo"].ToString()));
                        responsaveis.Add(int.Parse(dr["Responsavel"].ToString()));
                    }
                }
                cmd.CommandText = "select Nome from grupos where IDGrupo = @id";
                for (int i = 0; i < grupos.Count; i++)
                {
                    if (!cmd.Parameters.Contains("@id")) cmd.Parameters.AddWithValue("@id", grupos[i]);
                    else cmd.Parameters["@id"].Value = grupos[i];
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) cvm[i].Grupo = dr["Nome"].ToString();
                    }
                }
                cmd.CommandText = "select Nome, Totem from escuteiros where IDEscuteiro = @id";
                for (int i = 0; i < responsaveis.Count; i++)
                {
                    cmd.Parameters["@id"].Value = responsaveis[i];
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) cvm[i].Responsavel = dr["Totem"].ToString() + " - " + dr["Nome"].ToString();
                    }
                }
                cmd.Connection.Close();
            }
            return await Task.Run(() => View(cvm));
        }
        #region Nova caixa
        [HttpGet]
        public async Task<IActionResult> NovaCaixa()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (User.IsInRole("Comum")) return Forbid();
            if (TempData["inserir"] != null) TempData["inserirKeep"] = TempData["inserir"];
            using (MySqlCommand cmd = new MySqlCommand("select Nome from grupos;", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    List<string> grupos = new List<string>();
                    while (await dr.ReadAsync()) grupos.Add(dr["Nome"].ToString());
                    grupos.Sort();
                    ViewBag.grupos = grupos;
                }
                cmd.Connection.Close();
            }
            return await Task.Run(() => View());
        }
        public async Task<IActionResult> NovaCaixa(object model)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (!User.IsInRole("Administração de Agrupamento")) return Forbid();
            if (TempData["inserir"] != null) TempData["inserirKeep"] = TempData["inserir"];
            return await Task.Run(() => View("NovaCaixa", model));
        }
        [HttpPost]
        public async Task<IActionResult> NovaCaixa(CaixaViewModel cvm, bool? inserir = null)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (!User.IsInRole("Administração de Agrupamento")) return Forbid();
            if (ModelState.IsValid)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand((bool)inserir ? "insert into caixas(Nome, Grupo, Responsavel) values (@nome, @grupo, @responsavel);" : "update caixas set Nome = @nome, Grupo = @grupo, Responsavel = @responsavel where IDCaixa = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                    {
                        if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@nome", cvm.Nome);
                        using (MySqlCommand cmd2 = new MySqlCommand("select IDGrupo from grupos where Nome = @nome", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                        {
                            if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                            cmd2.Parameters.AddWithValue("@nome", cvm.Grupo);
                            await cmd2.PrepareAsync();
                            using (MySqlDataReader dr2 = cmd2.ExecuteReader())
                            {
                                if (!dr2.HasRows)
                                {
                                    ModelState.AddModelError("Grupo não existente", "O grupo selecionado não existe");
                                    ViewData.Add(new KeyValuePair<string, object>("Error", "A equipa selecionada não existe"));
                                    return await Task.Run(() => RedirectToAction("NovaCaixa"));
                                }
                                else while (await dr2.ReadAsync()) cmd.Parameters.AddWithValue("@grupo", int.Parse(dr2["IDGrupo"].ToString()));
                            }
                            cmd2.Parameters.Clear();
                            cmd2.CommandText = "select * from escuteiros where ";
                            if (int.TryParse(cvm.Responsavel, out _))
                            {
                                cmd2.CommandText += "IDEscuteiro = @id";
                                cmd2.Parameters.AddWithValue("@id", cvm.Responsavel);
                            }
                            else
                            {
                                cmd.CommandText += "Nome = @nome or Totem = @totem";
                                cmd2.Parameters.AddWithValue("@nome", cvm.Responsavel);
                                cmd2.Parameters.AddWithValue("@totem", cvm.Responsavel);
                            }
                            await cmd2.PrepareAsync();
                            using (MySqlDataReader dr2 = cmd2.ExecuteReader())
                            {
                                if (!dr2.HasRows)
                                {
                                    ModelState.AddModelError("Escuteiro não existente", "Não foi encontrado um escuteiro com o ID, nome ou totem fornecido. Por favor, forneça um ID, nome ou totem de escuteiro válido");
                                    return await Task.Run(() => RedirectToAction("NovaCaixa"));
                                }
                                else while (await dr2.ReadAsync()) cmd.Parameters.AddWithValue("@responsavel", int.Parse(dr2["IDEscuteiro"].ToString()));
                            }
                            cmd.Connection.Close();
                        }
                        if ((bool)inserir) cmd.Parameters.AddWithValue("@id", cvm.ID);
                        await cmd.PrepareAsync();
                        int i = await cmd.ExecuteNonQueryAsync();
                        cmd.Connection.Close();
                        if (i == 0) throw new Exception($"não foi encontrado nenhum registo com o ID \"{cvm.ID}\"");
                    }
                    return await Task.Run(() => RedirectToAction("Index"));
                }
                catch (Exception e)
                {
                    TempData["msg"] = "Ocorreu um erro com a edição do registo: " + e.Message;
                }
            }
            return await Task.Run(() => RedirectToAction("NovaCaixa"));
        }
        #endregion
        public async Task<IActionResult> Editar(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (!User.IsInRole("Administração de Agrupamento")) return Forbid();
            TempData["inserir"] = false;
            CaixaViewModel cvm = new CaixaViewModel();
            try
            {

                using (MySqlCommand cmd = new MySqlCommand("select Nome from grupos;", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        List<string> grupos = new List<string>();
                        while (await dr.ReadAsync()) grupos.Add(dr["Nome"].ToString());
                        grupos.Sort();
                        ViewBag.grupos = grupos;
                    }
                    cmd.Connection.Close();
                }
                using (MySqlCommand cmd = new MySqlCommand("select * from caixas where IDCaixa = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Prepare();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        if (!dr.HasRows)
                        {
                            cmd.Connection.Close();
                            throw new Exception($"não foi encontrado um registo com o ID \"{id}\"");
                        }
                        else
                        {
                            while (await dr.ReadAsync())
                            {
                                cvm.ID = id;
                                cvm.Nome = dr["Nome"].ToString();
                                cvm.Saldo = decimal.Parse(dr["Saldo"].ToString());
                                cvm.Grupo = dr["Grupo"].ToString();
                                cvm.Responsavel = dr["Responsavel"].ToString();
                            }
                        }
                    }
                    cmd.CommandText = "select Nome from escuteiros where IDEscuteiro = @id";
                    cmd.Parameters["@id"].Value = cvm.Responsavel;
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) cvm.Responsavel = dr["Nome"].ToString();
                    }
                    cmd.CommandText = "select Nome from grupos where IDGrupo = @id";
                    cmd.Parameters["@id"].Value = cvm.Grupo;
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) cvm.Grupo = dr["Nome"].ToString();
                    }
                    cmd.CommandText = cmd.CommandText.Remove(cmd.CommandText.IndexOf(" where IDGrupo = @id"));
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        List<string> grupos = new List<string>();
                        while (await dr.ReadAsync()) grupos.Add(dr["Nome"].ToString());
                        grupos.Sort();
                        ViewBag.grupos = grupos;
                    }
                    cmd.Connection.Close();
                }
                return await Task.Run(() => NovaCaixa((object)cvm));
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a edição do registo: " + e.Message;
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        #region Eliminar
        public async Task<IActionResult> Eliminar(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (!User.IsInRole("Administração de Agrupamento")) return Forbid();
            CaixaViewModel cvm = new CaixaViewModel();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("select caixas.*, grupos.Nome, escuteiros.Nome as NomeEscut from caixas inner join grupos on caixas.Grupo = grupos.IDGrupo inner join escuteiros on caixas.Responsavel = escuteiros.IDEscuteiro where IDCaixa = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        if (!dr.HasRows)
                        {
                            cmd.Connection.Close();
                            throw new Exception($"não existe nenhum registo com o ID \"{id}\"");
                        }
                        else
                        {
                            while (await dr.ReadAsync())
                            {
                                cvm.ID = int.Parse(dr["IDCaixa"].ToString());
                                cvm.Nome = dr["Nome"].ToString();
                                cvm.Grupo = $"{dr["Grupo"]} - {dr["Nome"]}";
                                cvm.Responsavel = $"{dr["Responsavel"]} - {dr["NomeEscut"]}";
                                cvm.Saldo = decimal.Parse(dr["Saldo"].ToString());
                            }
                        }
                    }
                    cmd.Connection.Close();
                }
                return await Task.Run(() => View("Eliminar", cvm));
            }
            catch (Exception e)
            {
                ViewBag["msg"] = "Ocorreu um erro com a eliminação do registo: " + e.Message;
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        [HttpPost]
        public async Task<IActionResult> EliminarPost(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (!User.IsInRole("Administração de Agrupamento")) return Forbid();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("delete from caixas where IDCaixa = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.PrepareAsync();
                    int i = await cmd.ExecuteNonQueryAsync();
                    cmd.Connection.Close();
                    if (i == 0) throw new Exception($"não existe nenhum registo com o ID \"{id}\"");
                }
            }
            catch (MySqlException mse)
            {
                if (mse.Number == 1451) TempData["msg"] = "esta caixa tem dados associados a si mesma, como movimentos referentes a esta. Procure esses dados e remova as ligações a esta caixa, de modo a eliminá-la com segurança.";
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