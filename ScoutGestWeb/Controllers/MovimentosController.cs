using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ScoutGestWeb.Models;

namespace ScoutGestWeb.Controllers
{
    public class MovimentosController : Controller
    {
        List<MovimentoViewModel> mvm = new List<MovimentoViewModel>();
        List<int> caixas = new List<int>();

        public async Task<IActionResult> Index()
        {
            if (UserData.UserData.userData.Count == 0) return await Task.Run(() => RedirectToAction("Index", "Home"));
            using (MySqlCommand cmd = new MySqlCommand("select * from movimentos where IDMovimento > 0;", UserData.UserData.con))
            {
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (await dr.ReadAsync())
                    {
                        mvm.Add(new MovimentoViewModel()
                        {
                            IDMovimento = int.Parse(dr["IDMovimento"].ToString()),
                            IDDocumento = dr["IDDocumento"].ToString(),
                            TipoMovimento = dr["TipoMovimento"].ToString() == "1" ? "Entrada de Tesouraria" : "Saída de Tesouraria",
                            DataHora = Convert.ToDateTime(dr["DataHora"].ToString()),
                            Valor = decimal.Parse(dr["Valor"].ToString()),
                            TipoPagamento = dr["TipoPag"].ToString(),
                            Descricao = dr["Descricao"].ToString()
                        });
                        caixas.Add(int.Parse(dr["IDCaixa"].ToString()));
                    }
                }
                cmd.CommandText = "select Nome from caixas where IDCaixa = @id";
                int i = 0;
                while (i < mvm.Count)
                {
                    cmd.Parameters.AddWithValue("@id", caixas[i]);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (await dr.ReadAsync()) mvm[i].IDCaixa = caixas[i] + dr["Nome"].ToString();
                    }
                    cmd.Parameters.Clear();
                    i++;
                }
            }
            return await Task.Run(() => View(mvm));
        }
    }
}