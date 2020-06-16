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
    public class AtividadesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (UserData.UserData.userData.Count == 0) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<AtividadeViewModel> avm = new List<AtividadeViewModel>();
            using (MySqlCommand cmd = new MySqlCommand("select IDAtividade, Nome, DataInicio, DataFim from atividades where Ativa = 1;", UserData.UserData.con))
            {
                if (UserData.UserData.con.State == ConnectionState.Closed) UserData.UserData.con.Open();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (await dr.ReadAsync())
                    {
                        avm.Add(new AtividadeViewModel()
                        {
                            IDAtividade = int.Parse(dr["IDAtividade"].ToString()),
                            Nome = dr["Nome"].ToString(),
                            DataInicio = DateTime.Parse(dr["DataInicio"].ToString()),
                            DataFim = DateTime.Parse(dr["DataInicio"].ToString())
                        });
                    }
                }
            }
            return await Task.Run(() => View(avm));
        }
        //[Route("Atividades/Detalhes/{id}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            if (UserData.UserData.userData.Count == 0) return await Task.Run(() => RedirectToAction("Index", "Home"));
            AtividadeViewModel avm = new AtividadeViewModel();
            using (MySqlCommand cmd = new MySqlCommand("select * from atividades where IDAtividade = @id", UserData.UserData.con))
            {
                if (UserData.UserData.con.State == ConnectionState.Closed) UserData.UserData.con.Open();
                cmd.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (await dr.ReadAsync())
                    {
                        avm.IDAtividade = int.Parse(dr["IDAtividade"].ToString());
                        avm.Nome = dr["Nome"].ToString();
                        avm.Tipo = dr["Tipo"].ToString();
                        avm.Seccao = int.Parse(dr["Seccao"].ToString());
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
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<IActionResult> InserirAtividade(AtividadeViewModel avm)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into atividades(Nome, Tipo, Seccao, Local, DataInicio, DataFim, Ativa) values (@nome, @tipo, @seccao, @local, @inicio, @fim, @ativa);", UserData.UserData.con))
                {
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
    }
    //what do u need me to do?
    //ha tanta cena p fzr e n sbs o q fzr? dou te uma dica: login
}