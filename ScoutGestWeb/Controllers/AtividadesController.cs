using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using ScoutGestWeb.Models;
namespace ScoutGestWeb.Controllers
{
    public class AtividadesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AtividadesController(UserManager<ApplicationUser> userManager) => _userManager = userManager;
        public async Task<IActionResult> Index(string coluna, string procura)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<AtividadeViewModel> avm = new List<AtividadeViewModel>();
            if (string.IsNullOrEmpty(coluna) && string.IsNullOrEmpty(procura))
            {
                using (MySqlCommand cmd = new MySqlCommand("select IDAtividade, Nome, DataInicio, DataFim from atividades where Ativa = 1" + (User.IsInRole("Equipa de Animação") || User.IsInRole("Comum") ? " and Seccao = @seccao or Seccao = \"Agrupamento\"" + (User.IsInRole("Comum") ? " and DataInicio between now() - interval 3 day and now() + interval 3 day" : "") : "") + (User.IsInRole("Comum") ? ";" : " order by DataInicio desc limit 25;"), new MySqlConnection("server =localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync(); ;
                    if (User.IsInRole("Equipa de Animação") || User.IsInRole("Comum"))
                    {
                        cmd.Parameters.AddWithValue("@seccao", (await _userManager.GetUserAsync(User)).Seccao);
                    }
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            avm.Add(new AtividadeViewModel()
                            {
                                IDAtividade = int.Parse(dr["IDAtividade"].ToString()),
                                Nome = dr["Nome"].ToString(),
                                DataInicio = DateTime.Parse(dr["DataInicio"].ToString()),
                                DataFim = DateTime.Parse(dr["DataFim"].ToString())
                            });
                        }
                    }
                }
            }
            return await Task.Run(() => View(avm));
        }
        [Route("Atividades/Detalhes/{id}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            AtividadeViewModel avm = new AtividadeViewModel();
            using (MySqlCommand cmd = new MySqlCommand("select * from atividades where IDAtividade = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();;
                cmd.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        avm.IDAtividade = int.Parse(dr["IDAtividade"].ToString());
                        avm.Nome = dr["Nome"].ToString();
                        avm.Tipo = dr["Tipo"].ToString();
                        avm.Seccao = dr["Seccao"].ToString();
                        avm.Local = dr["Local"].ToString();
                        avm.DataInicio = DateTime.Parse(dr["DataInicio"].ToString());
                        avm.DataFim = DateTime.Parse(dr["DataInicio"].ToString());
                    }
                }
            }
            return await Task.Run(() => View(avm));
        }
        public async Task<IActionResult> InserirAtividade()
        {
            return await Task.Run(() => !User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : (IActionResult)View());
        }
        [HttpPost]
        public async Task<IActionResult> InserirAtividade(AtividadeViewModel avm)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (ModelState.IsValid)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand("insert into atividades(Nome, Tipo, Seccao, Local, DataInicio, DataFim, Ativa) values (@nome, @tipo, @seccao, @local, @inicio, @fim, @ativa);", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                    {
                        if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
                        cmd.Parameters.AddWithValue("@nome", avm.Nome);
                        cmd.Parameters.AddWithValue("@tipo", avm.Tipo);
                        cmd.Parameters.AddWithValue("@seccao", avm.Seccao);
                        cmd.Parameters.AddWithValue("@local", avm.Local);
                        cmd.Parameters.AddWithValue("@inicio", avm.DataInicio);
                        cmd.Parameters.AddWithValue("@fim", avm.DataFim);
                        cmd.Parameters.AddWithValue("@ativa", avm.Ativa == true ? 1 : 0);
                        await cmd.PrepareAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                catch (MySqlException mse)
                {
                    ModelState.AddModelError("Erro", "Erro na inserção na base de dados: " + mse.ToString());
                    return await Task.Run(() => View());
                }
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => View());
        }
    }
}