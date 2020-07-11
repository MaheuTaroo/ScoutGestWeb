using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MySql.Data.MySqlClient;
using ScoutGestWeb.Models;

namespace ScoutGestWeb.Controllers
{
    [RequireHttps]
    public class TiposDocsController : Controller
    {
        bool insert = true;
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (TempData["msg"] != null) TempData.Keep("msg");
            List<TiposDocsViewModel> tdvm = new List<TiposDocsViewModel>();
            using (MySqlCommand cmd = new MySqlCommand("select * from tipos_docs where Descricao not like \"Teste\"", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
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
        #region Novo documento
        [HttpGet]
        public async Task<IActionResult> NovoDocumento()
        {
            return await Task.Run(() => !User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : (IActionResult)View());
        }
        public async Task<IActionResult> NovoDocumento(object model)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            TempData["insert"] = insert;
            return await Task.Run(() => View(model));
        }
        [HttpPost]
        public async Task<IActionResult> NovoDocumento(TiposDocsViewModel tdvm, string idold = null)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (ModelState.IsValid)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(insert ? "insert into tipos_docs values (@id, @descricao);" : "update tipos_docs set IDDocumento = @id and Descricao = @descricao where IDDocumento = @idold", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                    {
                        if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync(); ;
                        cmd.Parameters.AddWithValue("@id", tdvm.IDDocumento);
                        cmd.Parameters.AddWithValue("@descricao", tdvm.Descricao);
                        if (idold != null) cmd.Parameters.AddWithValue("@idold", idold);
                        await cmd.PrepareAsync();
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i == 0) throw new Exception($"não foi encontrado nenhum registo com o ID \"{idold}\"");
                        TempData["msg"] = "Tipo de documento " + (insert ? "inserido" : "alterado") + " com sucesso";
                    }
                }
                catch (Exception e)
                {
                    TempData["msg"] = "Ocorreu um erro com a inserção ou alteração do registo: " + e.Message;
                }
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => RedirectToAction("NovoDocumento"));
        }
        #endregion
        public async Task<IActionResult> Editar(string id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            TiposDocsViewModel tdvm = new TiposDocsViewModel();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from tipos_docs where IDDocumento = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        if (!dr.HasRows) throw new Exception($"não foi encontrado nenhum registo com o ID \"{id}\"");
                        else
                        {
                            insert = false;
                            while (await dr.ReadAsync())
                            {
                                tdvm.IDDocumento = dr["IDDocumento"].ToString();
                                tdvm.Descricao = dr["Descricao"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a edição do registo: " + e.Message;
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => RedirectToAction("NovoPagamento", (object)tdvm));
        }
        #region Eliminar
        public async Task<IActionResult> EliminarGet(string id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            TiposDocsViewModel tdvm = new TiposDocsViewModel();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from tipos_docs where IDDocumento = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed)cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        if (!dr.HasRows) throw new Exception($"não foi encontrado nenhum registo com o ID \"{id}\"");
                        else
                        {
                            while (await dr.ReadAsync())
                            {
                                tdvm.IDDocumento = dr["IDDocumento"].ToString();
                                tdvm.Descricao = dr["Descricao"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a eliminação do registo: " + e.Message;
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => View("Eliminar", tdvm));
        }
        [HttpPost] 
        public async Task<IActionResult> EliminarPost(string id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("delete from tipos_docs where IDDocumento = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.PrepareAsync();
                    int i = await cmd.ExecuteNonQueryAsync();
                    if (i == 0) throw new Exception($"não foi encontrado nenhum registo com o ID \"{id}\"");
                    else TempData["msg"] = "Tipo de documento eliminado com sucesso";
                }
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a eliminação do registo: " + e.Message;
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        #endregion
    }
}