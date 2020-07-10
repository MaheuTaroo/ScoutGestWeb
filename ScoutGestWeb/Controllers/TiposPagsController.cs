﻿using System;
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
            if (TempData["msg"] != null) TempData.Keep("msg");
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<TiposPagsViewModel> tpvm = new List<TiposPagsViewModel>();
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            using (MySqlCommand cmd = new MySqlCommand("select * from tipos_pags where IDPag not like \"00\";", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
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
        [HttpGet]
        public async Task<IActionResult> NovoPagamento()
        {
            return await Task.Run(() => !User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : (IActionResult)View());
        }
        public async Task<IActionResult> NovoPagamento(object model)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            TempData["insert"] = insert;
            return await Task.Run(() => View(model));
        }
        [HttpPost]
        public async Task<IActionResult> NovoPagamento(TiposPagsViewModel tpvm, string idold = null)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (ModelState.IsValid)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(insert ? "insert into tipos_pags values(@id, @pagamento);" : "update tipos_pags set IDPag = @id, Pagamento = @pagamento where IDPag = @idold", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                    {
                        if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@id", tpvm.IDPagamento);
                        cmd.Parameters.AddWithValue("@pagamento", tpvm.Pagamento);
                        if (idold == null) cmd.Parameters.AddWithValue("@idold", idold);
                        await cmd.PrepareAsync();
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i == 0) throw new Exception($"não foi encontrado um registo com o ID {idold}");
                        else TempData["msg"] = "Tipo de pagamento " + (insert ? "inserido" : "atualizado") + " com sucesso";
                    }
                }
                catch (Exception e)
                {
                    TempData["msg"] = "ocorreu um erro com a inserção ou alteração do registo: " + e.Message;
                }
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => RedirectToAction("NovoPagamento"));
        }
        #endregion
        #region Editar
        public async Task<IActionResult> Editar(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            TiposPagsViewModel tpvm = new TiposPagsViewModel();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from tipos_pags where IDPag = @id"))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        if (!dr.HasRows) throw new Exception($"não foi encontrado um registo com o ID \"{id}\"");
                        else
                        {
                            insert = false;
                            while (await dr.ReadAsync())
                            {
                                tpvm.IDPagamento = dr["IDPag"].ToString();
                                tpvm.Pagamento = dr["Pagamento"].ToString();
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
            return await Task.Run(() => RedirectToAction("NovoPagamento", (object)tpvm));
        }
        #endregion
        #region Eliminar
        public async Task<IActionResult> EliminarGet(string id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            TiposPagsViewModel tpvm = new TiposPagsViewModel();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from tipos_pags where IDPag = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        if (!dr.HasRows)
                        {
                            throw new Exception($"não foi encontrado nenhum registo com o ID \"{id}\"");
                        }
                        while (await dr.ReadAsync())
                        {
                            tpvm.IDPagamento = dr["IDPag"].ToString();
                            tpvm.Pagamento = dr["Pagamento"].ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro ao eliminar o registo: " + e.Message;
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => View(tpvm));
        }
        [HttpPost]
        public async Task<IActionResult> EliminarPost(string id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("delete from tipos_pags where IDPag = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.PrepareAsync();
                    int i = await cmd.ExecuteNonQueryAsync();
                    if (i == 0) throw new ApplicationException($"não existe nenhum registo com o ID \"{id}\"");
                    TempData["msg"] = "Tipo de pagamento apagado com sucesso";
                }
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro ao eliminar o registo: " + e.Message;
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        #endregion
    }
}