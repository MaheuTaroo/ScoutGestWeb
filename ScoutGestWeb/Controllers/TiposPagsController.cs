using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ScoutGestWeb.Models;

namespace ScoutGestWeb.Controllers
{
    public class TiposPagsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<TiposPagsViewModel> tpvm = new List<TiposPagsViewModel>();
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            using (MySqlCommand cmd = new MySqlCommand("select * from tipos_pags where IDPag not like \"00\";", UserData.UserData.con))
            using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
            {
                while (await dr.ReadAsync())
                {
                    tpvm.Add(new TiposPagsViewModel()
                    {
                        IDPagamento = dr["IDPag"].ToString(),
                        Pagamento = dr["Pagamento"].ToString()
                    });
                }
            }
            return await Task.Run(() => View(tpvm));
        }
        public async Task<IActionResult> NovoPagamento()
        {
            return await Task.Run(() => !User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : (IActionResult)View());
        }
        [HttpPost]
        public async Task<IActionResult> NovoPagamento(TiposPagsViewModel tpvm)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (ModelState.IsValid)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into tipos_pags values(@id, @pagamento);", UserData.UserData.con))
                {
                    cmd.Parameters.AddWithValue("@id", tpvm.IDPagamento);
                    cmd.Parameters.AddWithValue("@pagamento", tpvm.Pagamento);
                    await cmd.PrepareAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return await Task.Run(() => RedirectToAction("Index"));
                }
            }
            return await Task.Run(() => RedirectToAction("NovoPagamento"));
        }
    }
}