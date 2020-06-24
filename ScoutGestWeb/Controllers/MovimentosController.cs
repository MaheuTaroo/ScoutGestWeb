using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ScoutGestWeb.Models;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

namespace ScoutGestWeb.Controllers
{
    public class MovimentosController : Controller
    {
        private readonly List<MovimentoViewModel> mvm = new List<MovimentoViewModel>();
        private readonly List<int> caixas = new List<int>();
        private readonly UserManager<ApplicationUser> _userManager;
        public MovimentosController(UserManager<ApplicationUser> userManager) => _userManager = userManager;
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            using (MySqlCommand cmd = new MySqlCommand("select * from movimentos where IDMovimento > 0;", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
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
        public async Task<IActionResult> DiarioCaixa()
        {
            List<string> caixas = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand(("select IDCaixa, Nome from caixas where " + (User.IsInRole("Administração de Agrupamento") ? "IDCaixa > 0;" : "Grupo = " + (await _userManager.GetUserAsync(User)).IDGrupo)), new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) caixas.Add(dr["IDCaixa"].ToString() + " - " + dr["Nome"].ToString());
                }
            }
            ViewBag.caixas = caixas;
            return await Task.Run(() => View());
        }
        #region PDFs
        public async Task<IActionResult> PDFMovs(string caixa, DateTime? inicio, DateTime? fim)
        {
            List<MovimentoViewModel> listmvm = new List<MovimentoViewModel>();
            string _inicio = string.Format("{0}-{1}-{2} {3}:{4}:{5}", inicio != null ? new string[] { inicio.Value.Year.ToString(), inicio.Value.Month.ToString(), inicio.Value.Day.ToString(), inicio.Value.Hour.ToString(), inicio.Value.Minute.ToString(), inicio.Value.Second.ToString() } : new string[] { DateTime.MinValue.Year.ToString(), DateTime.MinValue.Month.ToString(), DateTime.MinValue.Day.ToString(), DateTime.MinValue.Hour.ToString(), DateTime.MinValue.Minute.ToString(), DateTime.MinValue.Second.ToString() }), _fim = string.Format("{0}-{1}-{2} {3}:{4}:{5}", fim != null ? new string[] { fim.Value.Year.ToString(), fim.Value.Month.ToString(), fim.Value.Day.ToString(), fim.Value.Hour.ToString(), fim.Value.Minute.ToString(), fim.Value.Second.ToString() } : new string[] { DateTime.MaxValue.Year.ToString(), DateTime.MaxValue.Month.ToString(), DateTime.MaxValue.Day.ToString(), DateTime.MaxValue.Hour.ToString(), DateTime.MaxValue.Minute.ToString(), DateTime.MaxValue.Second.ToString() });
            using (MySqlCommand cmd = new MySqlCommand("select * from movimentos where IDMovimento > 0 and IDDocumento not in (select IDDocumento from tipos_docs where Descricao like \"%ransferência%\") and IDCaixa = id and DataHora between @inicio and @fim;", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                cmd.Parameters.AddWithValue("@caixa", caixa.Substring(0, caixa.IndexOf(" - ")));
                cmd.Parameters.AddWithValue("@inicio", _inicio);
                cmd.Parameters.AddWithValue("@fim", _fim);
                await cmd.PrepareAsync();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) listmvm.Add(new MovimentoViewModel()
                    {
                        IDMovimento = int.Parse(dr["IDMovimento"].ToString()),
                        IDCaixa = dr["IDCaixa"].ToString(),
                        IDDocumento = dr["IDDocumento"].ToString(),
                        TipoMovimento = dr["TipoMovimento"].ToString() == "1" ? "Entrada" : dr["TipoMovimento"].ToString() == "2" ? "Saída" : "Teste",
                        User = await _userManager.FindByNameAsync(dr["User"].ToString()),
                        DataHora = Convert.ToDateTime(dr["DataHora"].ToString()),
                        Valor = decimal.Parse(dr["Valor"].ToString()),
                        TipoPagamento = dr["TipoPag"].ToString(),
                        Descricao = dr["Descricao"].ToString(),
                        Atividade = dr["Atividade"].ToString()
                    });
                }
            }
            return await Task.Run(() => new ViewAsPdf("Analises/MovimentosCaixa", listmvm)
            {
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(5, 5, 5, 5)
            });
        }
        public async Task<IActionResult> PDFCaixas(string caixaInicio, string caixaFim)
        {
            List<CaixaViewModel> cvm = new List<CaixaViewModel>();
            List<string> grupos = new List<string>(), escuteiros = new List<string>();
            List<int> gruposNum = new List<int>(), escutNum = new List<int>();
            using (MySqlCommand cmd = new MySqlCommand("select * from caixas where IDCaixa = @inicio and @fim", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        cvm.Add(new CaixaViewModel()
                        {
                            Nome = dr["Nome"].ToString(),
                            Saldo = decimal.Parse(dr["Saldo"].ToString())
                        });
                        gruposNum.Add(int.Parse(dr["Grupo"].ToString()));
                        escutNum.Add(int.Parse(dr["Responsavel"].ToString()));
                    }
                }
                cmd.CommandText = "select IDEscuteiro, Totem from escuteiros";
                for (int i = 0; i < mvm.Count; i++)
                {

                }
                cmd.CommandText = "select IDGrupo, Nome from grupos";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        if (cvm.Contains(new CaixaViewModel()
                        {
                            Grupo = dr["IDGrupo"].ToString()
                        })) escuteiros.Add(dr["Nome"].ToString());
                    }
                }
            }
            return await Task.Run(() => View("Analises/Caixas"));
        }
        public async Task<IActionResult> PDFPags()
        {
            List<TiposPagsViewModel> listtpvm = new List<TiposPagsViewModel>();
            using (MySqlCommand cmd = new MySqlCommand("select * from tipos_pags where IDPag not like \"00\";", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();;
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) listtpvm.Add(new TiposPagsViewModel()
                    {
                        IDPagamento = dr["IDPag"].ToString(),
                        Pagamento = dr["Pagamento"].ToString()
                    });
                }
            }
            return await Task.Run(() => new ViewAsPdf("Analises/TiposPags", listtpvm)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                PageMargins = new Rotativa.AspNetCore.Options.Margins(5, 5, 5, 5)
            });
        }
        public async Task<IActionResult> PDFTransfs()
        {
            List<MovimTransfViewModel> listmtvm = new List<MovimTransfViewModel>();
            using (MySqlCommand cmd = new MySqlCommand("select * from movimentos where IDMovimento > 0 and IDDocumento in (select IDDocumento from tipos_docs where Descricao like \"%ransferência%\") and TipoMovimento = 1;", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();;
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) listmtvm.Add(new MovimTransfViewModel()
                    {
                        IDMovimento = int.Parse(dr["IDMovimento"].ToString()),
                        IDCaixaDestino = dr["IDCaixa"].ToString(),
                        IDDocumento = dr["IDDocumento"].ToString(),
                        User = await _userManager.FindByNameAsync(dr["User"].ToString()),
                        DataHora = Convert.ToDateTime(dr["DataHora"].ToString()),
                        Valor = decimal.Parse(dr["Valor"].ToString()),
                        TipoPagamento = dr["TipoPag"].ToString(),
                        Descricao = dr["Descricao"].ToString(),
                        Atividade = dr["Atividade"].ToString()
                    });
                }
                cmd.CommandText = cmd.CommandText.Replace("TipoMovimento = 1", "TipoMovimento = 2");
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    foreach (MovimTransfViewModel item in listmtvm)
                    {
                        await dr.ReadAsync();
                        item.IDCaixaOrigem = dr["IDCaixa"].ToString();
                    }
                }
            }
            return await Task.Run(() => new ViewAsPdf("Analises/Transferencias", listmtvm)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                PageMargins = new Rotativa.AspNetCore.Options.Margins(5, 5, 5, 5)
            });
        }
        #endregion
        [HttpGet]
        public async Task<IActionResult> Entrada()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<string> nomesCaixas = new List<string>(), nomesDocs = new List<string>(), nomesPags = new List<string>(), nomesAtivs = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand("select IDCaixa, Nome from caixas where IDCaixa > 0", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesCaixas.Add(dr["IDCaixa"].ToString() + " - " + dr["Nome"].ToString());
                }
                cmd.CommandText = "select IDDocumento, Descricao from tipos_docs where IDDocumento not like \"00\"";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesDocs.Add(dr["IDDocumento"].ToString() + " - " + dr["Descricao"].ToString());
                }
                cmd.CommandText = "select * from tipos_pags where IDPag not like \"00\"";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesPags.Add(dr["IDPag"].ToString() + " - " + dr["Pagamento"].ToString());
                }
                cmd.CommandText = "select IDAtividade, Nome from atividades where IDAtividade > 0 and Ativa = 1;";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesAtivs.Add(dr["IDAtividade"].ToString() + " - " + dr["Nome"].ToString());
                }
            }
            ViewBag.caixas = nomesCaixas;
            ViewBag.documentos = nomesDocs;
            ViewBag.pagamentos = nomesPags;
            ViewBag.atividades = nomesAtivs;
            return await Task.Run(() => View());
        }
        public async Task<IActionResult> Entrada(object model)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<string> nomesCaixas = new List<string>(), nomesDocs = new List<string>(), nomesPags = new List<string>(), nomesAtivs = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand("select IDCaixa, Nome from caixas where IDCaixa > 0", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesCaixas.Add(dr["IDCaixa"].ToString() + " - " + dr["Nome"].ToString());
                }
                cmd.CommandText = "select IDDocumento, Descricao from tipos_docs where IDDocumento not like \"00\"";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesDocs.Add(dr["IDDocumento"].ToString() + " - " + dr["Descricao"].ToString());
                }
                cmd.CommandText = "select * from tipos_pags where IDPag not like \"00\"";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesPags.Add(dr["IDPag"].ToString() + " - " + dr["Pagamento"].ToString());
                }
                cmd.CommandText = "select IDAtividade, Nome from atividades where IDAtividade > 0 and Ativa = 1;";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesAtivs.Add(dr["IDAtividade"].ToString() + " - " + dr["Nome"].ToString());
                }
            }
            ViewBag.caixas = nomesCaixas;
            ViewBag.documentos = nomesDocs;
            ViewBag.pagamentos = nomesPags;
            ViewBag.atividades = nomesAtivs;
            return await Task.Run(() => View(model));
        }
        [HttpPost]
        public async Task<IActionResult> Entrada(MovimentoViewModel mvm)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            mvm.User = await _userManager.GetUserAsync(User);
            if (mvm.IDCaixa == null)
            {
                using (MySqlCommand cmd = new MySqlCommand("select IDCaixa from caixas where Grupo = @id;", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@id", mvm.User.IDGrupo);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) mvm.IDCaixa = dr["IDCaixa"].ToString();
                    }
                }
            }
            mvm.TipoMovimento = "Entrada";
            if (mvm.DataHora == DateTime.MinValue) mvm.DataHora = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(mvm);
            if (ModelState.IsValid)
            {
                using (MySqlCommand cmd = new MySqlCommand("select IDTipoMov from tipos_movs where Movimento = @tipo;", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
                    cmd.Parameters.AddWithValue("@tipo", mvm.TipoMovimento);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) mvm.TipoMovimento = dr["IDTipoMov"].ToString();
                    }
                }
                using (MySqlCommand cmd = new MySqlCommand("insert into movimentos(IDCaixa, IDDocumento, TipoMovimento, User, DataHora, Valor, TipoPag, Descricao, Atividade) values (@caixa, @documento, @tipomov, @user, @data, @valor, @tipopag, @descricao, atividade);", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
                    cmd.Parameters.AddWithValue("@caixa", mvm.IDCaixa.Contains(" - ") ? mvm.IDCaixa.Substring(0, mvm.IDCaixa.IndexOf(" - ")) : mvm.IDCaixa);
                    cmd.Parameters.AddWithValue("@documento", mvm.IDDocumento.Substring(0, mvm.IDDocumento.IndexOf(" - ")));
                    cmd.Parameters.AddWithValue("@tipomov", mvm.TipoMovimento);
                    cmd.Parameters.AddWithValue("@user", mvm.User.NormalizedUserName);
                    cmd.Parameters.AddWithValue("@data", mvm.DataHora);
                    cmd.Parameters.AddWithValue("@valor", mvm.Valor);
                    cmd.Parameters.AddWithValue("@tipopag", mvm.TipoPagamento);
                    cmd.Parameters.AddWithValue("@descricao", mvm.Descricao);
                    cmd.Parameters.AddWithValue("@atividade", mvm.Atividade.Substring(0, mvm.Atividade.IndexOf(" - ")));
                    await cmd.PrepareAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => Entrada((object)mvm));
        }
        [HttpGet]
        public async Task<IActionResult> Saida()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<string> nomesCaixas = new List<string>(), nomesDocs = new List<string>(), nomesPags = new List<string>(), nomesAtivs = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand("select IDCaixa, Nome from caixas where IDCaixa > 0", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesCaixas.Add(dr["IDCaixa"].ToString() + " - " + dr["Nome"].ToString());
                }
                cmd.CommandText = "select IDDocumento, Descricao from tipos_docs where IDDocumento not like \"00\"";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesDocs.Add(dr["IDDocumento"].ToString() + " - " + dr["Descricao"].ToString());
                }
                cmd.CommandText = "select * from tipos_pags where IDPag not like \"00\"";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesPags.Add(dr["IDPag"].ToString() + " - " + dr["Pagamento"].ToString());
                }
                cmd.CommandText = "select IDAtividade, Nome from atividades where IDAtividade > 0 and Ativa = 1;";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesAtivs.Add(dr["IDAtividade"].ToString() + " - " + dr["Nome"].ToString());
                }
            }
            ViewBag.caixas = nomesCaixas;
            ViewBag.documentos = nomesDocs;
            ViewBag.pagamentos = nomesPags;
            ViewBag.atividades = nomesAtivs;
            return await Task.Run(() => View());
        }
        public async Task<IActionResult> Saida(object model)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<string> nomesCaixas = new List<string>(), nomesDocs = new List<string>(), nomesPags = new List<string>(), nomesAtivs = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand("select IDCaixa, Nome from caixas where IDCaixa > 0", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesCaixas.Add(dr["IDCaixa"].ToString() + " - " + dr["Nome"].ToString());
                }
                cmd.CommandText = "select IDDocumento, Descricao from tipos_docs where IDDocumento not like \"00\"";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesDocs.Add(dr["IDDocumento"].ToString() + " - " + dr["Descricao"].ToString());
                }
                cmd.CommandText = "select * from tipos_pags where IDPag not like \"00\"";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesPags.Add(dr["IDPag"].ToString() + " - " + dr["Pagamento"].ToString());
                }
                cmd.CommandText = "select IDAtividade, Nome from atividades where IDAtividade > 0 and Ativa = 1;";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesAtivs.Add(dr["IDAtividade"].ToString() + " - " + dr["Nome"].ToString());
                }
            }
            ViewBag.caixas = nomesCaixas;
            ViewBag.documentos = nomesDocs;
            ViewBag.pagamentos = nomesPags;
            ViewBag.atividades = nomesAtivs;
            return await Task.Run(() => View(model));
        }
        [HttpPost]
        public async Task<IActionResult> Saida(MovimentoViewModel mvm)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            mvm.TipoMovimento = "Saida";
            mvm.User = await _userManager.GetUserAsync(User);
            if (mvm.DataHora == DateTime.MinValue) mvm.DataHora = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(mvm);
            if (ModelState.IsValid)
            {
                using (MySqlCommand cmd = new MySqlCommand("select IDTipoMov from tipos_movs where Movimento = @tipo;", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
                    cmd.Parameters.AddWithValue("@tipo", mvm.TipoMovimento);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) mvm.TipoMovimento = dr["IDTipoMov"].ToString();
                    }
                }
                using (MySqlCommand cmd = new MySqlCommand("insert into movimentos(IDCaixa, IDDocumento, TipoMovimento, User, DataHora, Valor, TipoPag, Descricao, Atividade) values (@caixa, @documento, @tipomov, @user, @data, @valor, @tipopag, @descricao, @atividade);", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
                    cmd.Parameters.AddWithValue("@caixa", mvm.IDCaixa.Substring(0, mvm.IDCaixa.IndexOf(" - ")));
                    cmd.Parameters.AddWithValue("@documento", mvm.IDDocumento.Substring(0, mvm.IDDocumento.IndexOf(" - ")));
                    cmd.Parameters.AddWithValue("@tipomov", mvm.TipoMovimento);
                    cmd.Parameters.AddWithValue("@user", mvm.User.NormalizedUserName);
                    cmd.Parameters.AddWithValue("@data", mvm.DataHora);
                    cmd.Parameters.AddWithValue("@valor", mvm.Valor);
                    cmd.Parameters.AddWithValue("@tipopag", mvm.TipoPagamento);
                    cmd.Parameters.AddWithValue("@descricao", mvm.Descricao);
                    cmd.Parameters.AddWithValue("@atividade", mvm.Atividade.Substring(0, mvm.Atividade.IndexOf(" - ")));
                    await cmd.PrepareAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => Saida((object)mvm));
        }
        [HttpGet]
        public async Task<IActionResult> Transferencia()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<string> nomesCaixas = new List<string>(), nomesDocs = new List<string>(), nomesPags = new List<string>(), nomesAtivs = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand("select IDCaixa, Nome from caixas where IDCaixa > 0", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesCaixas.Add(dr["IDCaixa"].ToString() + " - " + dr["Nome"].ToString());
                }
                cmd.CommandText = "select IDDocumento, Descricao from tipos_docs where IDDocumento not like \"00\"";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesDocs.Add(dr["IDDocumento"].ToString() + " - " + dr["Descricao"].ToString());
                }
                cmd.CommandText = "select * from tipos_pags where IDPag not like \"00\"";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesPags.Add(dr["IDPag"].ToString() + " - " + dr["Pagamento"].ToString());
                }
                cmd.CommandText = "select IDAtividade, Nome from atividades where IDAtividade > 0 and Ativa = 1;";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesAtivs.Add(dr["IDAtividade"].ToString() + " - " + dr["Nome"].ToString());
                }
            }
            ViewBag.caixas = nomesCaixas;
            ViewBag.documentos = nomesDocs;
            ViewBag.pagamentos = nomesPags;
            ViewBag.atividades = nomesAtivs;
            return await Task.Run(() => View());
        }
        public async Task<IActionResult> Transferencia(object model)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<string> nomesCaixas = new List<string>(), nomesDocs = new List<string>(), nomesPags = new List<string>(), nomesAtivs = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand("select IDCaixa, Nome from caixas where IDCaixa > 0", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesCaixas.Add(dr["IDCaixa"].ToString() + " - " + dr["Nome"].ToString());
                }
                cmd.CommandText = "select IDDocumento, Descricao from tipos_docs where IDDocumento not like \"00\"";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesDocs.Add(dr["IDDocumento"].ToString() + " - " + dr["Descricao"].ToString());
                }
                cmd.CommandText = "select * from tipos_pags where IDPag not like \"00\"";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesPags.Add(dr["IDPag"].ToString() + " - " + dr["Pagamento"].ToString());
                }
                cmd.CommandText = "select IDAtividade, Nome from atividades where IDAtividade > 0 and Ativa = 1;";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) nomesAtivs.Add(dr["IDAtividade"].ToString() + " - " + dr["Nome"].ToString());
                }
            }
            ViewBag.caixas = nomesCaixas;
            ViewBag.documentos = nomesDocs;
            ViewBag.pagamentos = nomesPags;
            ViewBag.atividades = nomesAtivs;
            return await Task.Run(() => View(model));
        }
        [HttpPost]
        public async Task<IActionResult> Transferencia(MovimTransfViewModel mtvm)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            mtvm.User = await _userManager.GetUserAsync(User);
            if (mtvm.DataHora == DateTime.MinValue) mtvm.DataHora = DateTime.Now;
            if (mtvm.IDCaixaOrigem == null)
            {
                using (MySqlCommand cmd = new MySqlCommand("select IDCaixa from caixas where Grupo = @grupo", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();;
                    cmd.Parameters.AddWithValue("@grupo", mtvm.User.IDGrupo);
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) mtvm.IDCaixaOrigem = dr["IDCaixa"].ToString();
                    }
                }
            }
            mtvm.TipoMovimento = "Saída de tesouraria";
            ModelState.Clear();
            TryValidateModel(mtvm);
            if (ModelState.IsValid)
            {
                if (mtvm.IDCaixaOrigem == mtvm.IDCaixaDestino)
                {
                    ModelState.AddModelError("", "A caixa de origem e a caixa de destino são iguais. Por favor, selecione caixas diferentes para a transferência de tesouraria");
                    return await Task.Run(() => Transferencia((object)mtvm));
                }
                using (MySqlCommand cmd = new MySqlCommand("insert into movimentos(IDCaixa, IDDocumento, TipoMovimento, User, DataHora, Valor, TipoPag, Descricao, Atividade) values (@caixa, @documento, @tipomov, @user, @data, @valor, @tipopag, @descricao, @atividade);", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();;
                    cmd.Parameters.AddWithValue("@caixa", mtvm.IDCaixaOrigem.Contains(" - ") ? mtvm.IDCaixaOrigem.Substring(0, mtvm.IDCaixaOrigem.IndexOf(" - ")) : mtvm.IDCaixaOrigem);
                    cmd.Parameters.AddWithValue("@documento", mtvm.IDDocumento.Substring(0, mtvm.IDDocumento.IndexOf(" - ")));
                    using (MySqlCommand cmd2 = new MySqlCommand("select IDTipoMov from tipos_movs where Movimento = @id;", cmd.Connection))
                    {
                        cmd2.Parameters.AddWithValue("@id", mtvm.TipoMovimento);
                        await cmd2.PrepareAsync();
                        using (MySqlDataReader dr = (MySqlDataReader)await cmd2.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                var v = dr["IDTipoMov"].GetType();
                                cmd.Parameters.AddWithValue("@tipomov", int.Parse(dr["IDTipoMov"].ToString()));
                            }
                        }
                    }
                    cmd.Parameters.AddWithValue("@user", mtvm.User.NormalizedUserName);
                    cmd.Parameters.AddWithValue("@data", mtvm.DataHora);
                    cmd.Parameters.AddWithValue("@valor", mtvm.Valor);
                    cmd.Parameters.AddWithValue("@tipopag", mtvm.TipoPagamento);
                    cmd.Parameters.AddWithValue("@descricao", mtvm.Descricao);
                    cmd.Parameters.AddWithValue("@atividade", mtvm.Atividade.Substring(0, mtvm.Atividade.IndexOf(" - ")));
                    await cmd.PrepareAsync();
                    await cmd.ExecuteNonQueryAsync();
                    using (MySqlCommand cmd2 = new MySqlCommand("select IDTipoMov from tipos_movs where Movimento = @id;", cmd.Connection))
                    {
                        cmd2.Parameters.AddWithValue("@id", "Entrada de tesouraria");
                        await cmd2.PrepareAsync();
                        using (MySqlDataReader dr = (MySqlDataReader)await cmd2.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync()) cmd.Parameters["@tipomov"].Value = int.Parse(dr["IDTipoMov"].ToString());
                        }
                    }
                    cmd.Parameters["@caixa"].Value = mtvm.IDCaixaDestino.Substring(0, mtvm.IDCaixaDestino.IndexOf(" - "));
                    await cmd.PrepareAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => Transferencia((object)mtvm));
        }
    }
}