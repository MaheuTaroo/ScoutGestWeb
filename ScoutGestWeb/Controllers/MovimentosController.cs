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
        private readonly List<MovimentoViewModel> mvm = new List<MovimentoViewModel>();
        private readonly List<int> caixas = new List<int>();
        public async Task<IActionResult> Index()
        {
            if (UserData.UserData.userData.Count == 0/* || Request.Cookies["User"] == null*/) return await Task.Run(() => RedirectToAction("Index", "Home"));
            using (MySqlCommand cmd = new MySqlCommand("select * from movimentos where IDMovimento > 0;", UserData.UserData.con))
            {
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
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
                for(int i = 0; i < mvm.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@id", caixas[i]);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) mvm[i].IDCaixa = caixas[i] + " - " + dr["Nome"].ToString();
                    }
                    cmd.Parameters.Clear();
                }
            }
            return await Task.Run(() => View(mvm));
        }
        public async Task<IActionResult> Entrada()
        {
            if (UserData.UserData.userData.Count == 0/* || Request.Cookies["User"] == null*/) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<string> nomesCaixas = new List<string>(), nomesDocs = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand("select IDCaixa, Nome from caixas where IDCaixa > 0", UserData.UserData.con))
            {
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesCaixas.Add(dr["IDCaixa"].ToString() + " - " + dr["Nome"].ToString());
                }
                cmd.CommandText = "select IDDocumento, Descricao from tipos_docs where IDDocumento not like \"00\"";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesDocs.Add(dr["IDDocumento"].ToString() + " - " + dr["Descricao"].ToString());
                }
            }
            ViewBag.caixas = nomesCaixas;
            ViewBag.documentos = nomesDocs;
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<IActionResult> Entrada(MovimentoViewModel mvm)
        {
            mvm.TipoMovimento = "Entrada";
            mvm.IDDocumento = mvm.IDDocumento.Substring(0, mvm.IDDocumento.IndexOf(" - "));
            ModelState.Clear();
            TryValidateModel(mvm);
            if (ModelState.IsValid)
            {
                using (MySqlCommand cmd = new MySqlCommand("select IDTipoMov from tipos_movs where Movimento = @tipo;", UserData.UserData.con))
                {
                    cmd.Parameters.AddWithValue("@tipo", mvm.TipoMovimento);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) mvm.TipoMovimento = dr["IDTipoMov"].ToString();
                    }
                }
                using (MySqlCommand cmd = new MySqlCommand("insert into movimentos(IDCaixa, IDDocumento, TipoMovimento, User, DataHora, Valor, TipoPag, Descricao) values (@caixa, @documento, @tipomov, @user, @data, @valor, @tipopag, @descricao);", UserData.UserData.con))
                {
                    cmd.Parameters.AddWithValue("@caixa", mvm.IDCaixa.Substring(0, mvm.IDCaixa.IndexOf(" - ")));
                    cmd.Parameters.AddWithValue("@documento", mvm.IDDocumento);
                    cmd.Parameters.AddWithValue("@tipomov", mvm.TipoMovimento);
                    cmd.Parameters.AddWithValue("@user", mvm.User);
                    cmd.Parameters.AddWithValue("@data", mvm.DataHora);
                    cmd.Parameters.AddWithValue("@valor", mvm.Valor);
                    cmd.Parameters.AddWithValue("@tipopag", mvm.TipoPagamento);
                    cmd.Parameters.AddWithValue("@descricao", mvm.Descricao);
                    await cmd.PrepareAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => Entrada());
        }
        public async Task<IActionResult> Saida()
        {
            if (UserData.UserData.userData.Count == 0/* || Request.Cookies["User"] == null*/) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<string> nomesCaixas = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand("select IDCaixa, Nome from caixas where IDCaixa > 0", UserData.UserData.con))
            using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
            {
                while (await dr.ReadAsync()) nomesCaixas.Add(dr["IDCaixa"].ToString() + " - " + dr["Nome"].ToString());
            }
            ViewBag.caixas = nomesCaixas;
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<IActionResult> Saida(MovimentoViewModel mvm)
        {
            mvm.TipoMovimento = "Saida";
            mvm.IDDocumento = mvm.IDDocumento.Substring(0, mvm.IDDocumento.IndexOf(" - "));
            ModelState.Clear();
            TryValidateModel(mvm);
            if (ModelState.IsValid)
            {
                using (MySqlCommand cmd = new MySqlCommand("select IDTipoMov from tipos_movs where Movimento = @tipo;", UserData.UserData.con))
                {
                    cmd.Parameters.AddWithValue("@tipo", mvm.TipoMovimento);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) mvm.TipoMovimento = dr["IDTipoMov"].ToString();
                    }
                }
                using (MySqlCommand cmd = new MySqlCommand("insert into movimentos(IDCaixa, IDDocumento, TipoMovimento, User, DataHora, Valor, TipoPag, Descricao) values (@caixa, @documento, @tipomov, @user, @data, @valor, @tipopag, @descricao);", UserData.UserData.con))
                {
                    cmd.Parameters.AddWithValue("@caixa", mvm.IDCaixa.Substring(0, mvm.IDCaixa.IndexOf(" - ")));
                    cmd.Parameters.AddWithValue("@documento", mvm.IDDocumento);
                    cmd.Parameters.AddWithValue("@tipomov", mvm.TipoMovimento);
                    cmd.Parameters.AddWithValue("@user", mvm.User);
                    cmd.Parameters.AddWithValue("@data", mvm.DataHora);
                    cmd.Parameters.AddWithValue("@valor", mvm.Valor);
                    cmd.Parameters.AddWithValue("@tipopag", mvm.TipoPagamento);
                    cmd.Parameters.AddWithValue("@descricao", mvm.Descricao);
                    await cmd.PrepareAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => Saida());
        }
        public async Task<IActionResult> Transferencia()
        {
            if (UserData.UserData.userData.Count == 0/* || Request.Cookies["User"] == null*/) return await Task.Run(() => RedirectToAction("Index", "Home"));
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<IActionResult> Transferencia(MovimTransfViewModel mtvm)
        {
            mtvm.TipoMovimento = "Saída";
            ModelState.Clear();
            TryValidateModel(mtvm);
            if (ModelState.IsValid)
            {
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => RedirectToAction("Transferencia"));
        }
    }
}