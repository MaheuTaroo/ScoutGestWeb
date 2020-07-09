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
    [RequireHttps]
    public class TiposPagsController : Controller
    {
        bool insert = true;
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<TiposPagsViewModel> tpvm = new List<TiposPagsViewModel>();
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            using (MySqlCommand cmd = new MySqlCommand("select * from tipos_pags where IDPag not like \"00\";", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
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
            }
            return await Task.Run(() => View(tpvm));
        }
        #region Novo pagamento
        public async Task<IActionResult> NovoPagamento()
        {
            return await Task.Run(() => !User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : (IActionResult)View());
        }
        public async Task<IActionResult> NovoPagamento(object model)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            return await Task.Run(() => View(model));
        }
        [HttpPost]
        public async Task<IActionResult> NovoPagamento(TiposPagsViewModel tpvm)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (ModelState.IsValid)
            {
                using (MySqlCommand cmd = new MySqlCommand(insert ? "insert into tipos_pags values(@id, @pagamento);" : "update tipos_pags set IDPag = @id, Pagamento = @pagamento where IDPag = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@id", tpvm.IDPagamento);
                    cmd.Parameters.AddWithValue("@pagamento", tpvm.Pagamento);
                    await cmd.PrepareAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return await Task.Run(() => RedirectToAction("Index"));
                }
            }
            return await Task.Run(() => RedirectToAction("NovoPagamento"));
        }
        #endregion
        #region Editar
        public async Task<IActionResult> Editar(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            insert = false;
            TiposPagsViewModel tpvm = new TiposPagsViewModel();
            using (MySqlCommand cmd = new MySqlCommand("select * from tipos_pags where IDPag = @id"))
            {
                if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@id", id);
                await cmd.PrepareAsync();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    tpvm.IDPagamento = dr["IDPag"].ToString();
                    tpvm.Pagamento = dr["Pagamento"].ToString();
                }
            }
            return await Task.Run(() => RedirectToAction("NovoPagamento", (object)tpvm));
        }
        #endregion
        #region Eliminar
        public async Task<IActionResult> EliminarGet(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            TiposPagsViewModel tpvm = new TiposPagsViewModel();
            using (MySqlCommand cmd = new MySqlCommand("select * from tipos_pags where IDPag = @id"))
            {
                if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@id", id);
                await cmd.PrepareAsync();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    tpvm.IDPagamento = dr["IDPag"].ToString();
                    tpvm.Pagamento = dr["Pagamento"].ToString();
                }
            }
            return await Task.Run(() => View(tpvm));
        }
        [HttpPost]
        public async Task<IActionResult> EliminarPost(int id)
        {
            return null;
        }
        #endregion
    }
}