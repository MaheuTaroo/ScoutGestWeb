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
    public class TiposDocsController : Controller
    {
        bool insert = true;
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (TempData["msg"] != null) TempData["msgKeep"] = TempData["msg"];
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
                cmd.Connection.Close();
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
            TempData["inserir"] = insert;
            return await Task.Run(() => View("NovoDocumento", model));
        }
        [HttpPost]
        public async Task<IActionResult> NovoDocumento(TiposDocsViewModel tdvm, string idold = null, bool? inserir = null)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (ModelState.IsValid)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand((bool)inserir? "insert into tipos_docs values (@id, @descricao);" : "update tipos_docs set IDDocumento = @id and Descricao = @descricao where IDDocumento = @idold", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                    {
                        if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@id", tdvm.IDDocumento);
                        cmd.Parameters.AddWithValue("@descricao", tdvm.Descricao);
                        if (idold != null) cmd.Parameters.AddWithValue("@idold", idold);
                        await cmd.PrepareAsync();
                        int i = await cmd.ExecuteNonQueryAsync();
                        cmd.Connection.Close();
                        if (i == 0) throw new Exception($"não foi encontrado nenhum registo com o ID \"{idold}\"");
                        TempData["msg"] = "Tipo de documento " + ((bool)inserir ? "inserido" : "alterado") + " com sucesso";
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
                        if (!dr.HasRows)
                        {
                            cmd.Connection.Close();
                            throw new Exception($"não foi encontrado nenhum registo com o ID \"{id}\"");
                        }
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
                    cmd.Connection.Close();
                }
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a edição do registo: " + e.Message;
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => NovoDocumento((object)tdvm));
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
                    if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        if (!dr.HasRows)
                        {
                            cmd.Connection.Close();
                            throw new Exception($"não foi encontrado nenhum registo com o ID \"{id}\"");
                        }
                        else
                        {
                            while (await dr.ReadAsync())
                            {
                                tdvm.IDDocumento = dr["IDDocumento"].ToString();
                                tdvm.Descricao = dr["Descricao"].ToString();
                            }
                        }
                    }
                    cmd.Connection.Close();
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
                    cmd.Connection.Close();
                    if (i == 0) throw new Exception($"não foi encontrado nenhum registo com o ID \"{id}\"");
                    else TempData["msg"] = "Tipo de documento eliminado com sucesso";
                }
            }
            catch (MySqlException mse)
            {
                if (mse.Number == 1451) TempData["msg"] = "este documentos tem dados associados a si mesmo, como movimentos referentes a este. Procure esses dados e remova as ligações a este documento, de modo a eliminá-lo com segurança.";
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