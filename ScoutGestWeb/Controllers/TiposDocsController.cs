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
    public class TiposDocsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<TiposDocsViewModel> tdvm = new List<TiposDocsViewModel>();
            using (MySqlCommand cmd = new MySqlCommand("select * from tipos_docs where Descricao not like \"Teste\"", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) tdvm.Add(new TiposDocsViewModel()
                    {
                        IDDocumento = dr["IDDocumento"].ToString(),
                        Descricao = dr["Descricao"].ToString()
                    });
                }
            }
            return await Task.Run(() => View(tdvm));
        }
        public async Task<IActionResult> NovoDocumento()
        {
            return await Task.Run(() => !User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : (IActionResult)View());
        }
        [HttpPost]
        public async Task<IActionResult> NovoDocumento(TiposDocsViewModel tdvm)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (ModelState.IsValid)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into tipos_docs values (@id, @descricao);", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
                    cmd.Parameters.AddWithValue("@id", tdvm.IDDocumento);
                    cmd.Parameters.AddWithValue("@descricao", tdvm.Descricao);
                    await cmd.PrepareAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => RedirectToAction("NovoDocumento"));
        }
    }
}