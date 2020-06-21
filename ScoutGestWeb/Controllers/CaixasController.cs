using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ScoutGestWeb.Models;

namespace ScoutGestWeb.Controllers
{
    public class CaixasController : Controller
    {
        private readonly List<CaixaViewModel> cvm = new List<CaixaViewModel>();
        private readonly List<int> grupos = new List<int>(), responsaveis = new List<int>();
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            using (MySqlCommand cmd = new MySqlCommand("select * from caixas", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        cvm.Add(new CaixaViewModel()
                        {
                            ID = int.Parse(dr["IDCaixa"].ToString()),
                            Nome = dr["Nome"].ToString(),
                            Responsavel = dr["Responsavel"].ToString() + " - "
                        });
                        grupos.Add(int.Parse(dr["Grupo"].ToString()));
                        responsaveis.Add(int.Parse(dr["Responsavel"].ToString()));
                    }
                }
                cmd.CommandText = "select Nome from grupos where IDGrupo = @id";
                for (int i = 0; i < grupos.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@id", grupos[i]);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) cvm[i].Grupo = dr["Nome"].ToString();
                    }
                    cmd.Parameters.Clear();
                }
                cmd.CommandText = "select Nome from escuteiros where IDEscuteiro = @id";
                for (int i = 0; i < responsaveis.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@id", grupos[i]);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) cvm[i].Responsavel += dr["Nome"].ToString();
                    }
                    cmd.Parameters.Clear();
                }
            }
            return await Task.Run(() => View(cvm));
        }
        public async Task<IActionResult> NovaCaixa()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            using (MySqlCommand cmd = new MySqlCommand("select Nome from grupos;", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    List<string> grupos = new List<string>();
                    while (await dr.ReadAsync()) grupos.Add(dr["Nome"].ToString());
                    grupos.Sort();
                    ViewBag.grupos = grupos;
                }
            }
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<IActionResult> NovaCaixa(CaixaViewModel cvm)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (ModelState.IsValid)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into caixas(Nome, Grupo, Responsavel) values (@nome, @grupo, @responsavel);", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@nome", cvm.Nome);
                    using (MySqlCommand cmd2 = new MySqlCommand("select IDGrupo from grupos where Nome = @nome", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                    {
                        if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
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
                       
                        ///try
                        ///{
                        ///    cmd.Parameters.AddWithValue("@id", int.Parse(cvm.Responsavel));
                        ///}
                        ///catch (FormatException)
                        ///{
                        ///    cmd.CommandText = cmd.CommandText.Replace("IDEscuteiro = @id", "Nome = @nome or Totem = @totem");
                        ///    cmd.Parameters.AddWithValue("@nome", cvm.Responsavel);
                        ///    cmd.Parameters.AddWithValue("@totem", cvm.Responsavel);
                        ///}
                        ///cmd.Prepare();
                        ///using (MySqlDataReader dr = cmd.ExecuteReader())
                        ///{
                        ///    if (!dr.HasRows)
                        ///    {
                        ///        ViewBag.Error = "Não foi encontrado um escuteiro com o ID, nome ou totem fornecido. Por favor, forneça um ID, nome ou totem de escuteiro válido";
                        ///        return RedirectToAction("NovaCaixa");
                        ///    }
                        ///}
                      
                    }
                    await cmd.PrepareAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => RedirectToAction("NovaCaixa"));
        }
    }
}




//aproveita e v se o botao ja ta ao lado da imagem
//n ta
//Martins consegui passar o texto e o titulo, mas da maneira que eu fiz não dá para passar o botão
//Pq form não dá para meter dentro de um p, mas vou tentar mudar para um div para ver se consigo