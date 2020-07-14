using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using ScoutGestWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutGestWeb.Controllers
{
    [RequireHttps]
    public class MovimentosController : Controller
    {
        #region Variáveis 
        private readonly List<MovimentoViewModel> mvm = new List<MovimentoViewModel>();
        private readonly List<int> caixas = new List<int>();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly List<string> listcaixas = new List<string>(), listpags = new List<string>(), listativs = new List<string>(), listseccoes = new List<string>();
        bool insert = true;
        #endregion
        public MovimentosController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            using (MySqlCommand cmd = new MySqlCommand("select IDCaixa, Nome from caixas where IDCaixa > 0", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) listcaixas.Add(dr["IDCaixa"].ToString() + " - " + dr["Nome"].ToString());
                }
                cmd.CommandText = "select * from tipos_pags where IDPag not like \"00\";";
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) listpags.Add(dr["IDPag"].ToString() + " - " + dr["Pagamento"].ToString());
                }
                cmd.CommandText = "select IDAtividade, Nome from atividades where IDAtividade > 0;";
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) listativs.Add(dr["IDAtividade"].ToString() + " - " + dr["Nome"].ToString());
                }
                listseccoes.AddRange(new string[6] { "Lobitos", "Exploradores", "Pioneiros", "Caminheiros", "Dirigentes", "Agrupamento" });
            }
            listcaixas = listcaixas.OrderBy(x => x.Substring(0, x.IndexOf(" - ")).Length).ToList();
            listpags = listpags.OrderBy(x => x.Substring(0, x.IndexOf(" - ")).Length).ToList();
            listativs = listativs.OrderBy(x => x.Substring(0, x.IndexOf(" - ")).Length).ToList();
        }
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (TempData["msg"] != null) TempData.Keep("msg");
            if (User.IsInRole("Administração de Agrupamento"))
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from movimentos where IDMovimento > 0;", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync(); ;
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
                    for (int i = 0; i < mvm.Count; i++)
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
            if (User.IsInRole("Equipa de Animação"))
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from movimentos where IDMovimento > 0 and IDCaixa in (select IDCaixa from caixas where Grupo in (select IDGrupo from grupos where Seccao = @seccao) and Grupo = @grupo);", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@seccao", (await _userManager.GetUserAsync(User)).Seccao);
                    cmd.Parameters.AddWithValue("@grupo", (await _userManager.GetUserAsync(User)).IDGrupo);
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
                    for (int i = 0; i < mvm.Count; i++)
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
            using (MySqlCommand cmd = new MySqlCommand("select * from movimentos where IDMovimento > 0 and IDCaixa in (select IDCaixa from caixas where Grupo = @id);", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                var user = await _userManager.GetUserAsync(User);
                cmd.Parameters.AddWithValue("@id", user.IDGrupo);
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        mvm.Add(new MovimentoViewModel()
                        {
                            IDMovimento = int.Parse(dr["IDMovimento"].ToString()),
                            IDDocumento = dr["IDDocumento"].ToString(),
                            Seccao = dr["Seccao"].ToString(),
                            TipoMovimento = dr["TipoMovimento"].ToString() == "1" ? "Entrada de Tesouraria" : "Saída de Tesouraria",
                            DataHora = Convert.ToDateTime(dr["DataHora"].ToString()),
                            Valor = decimal.Parse(dr["Valor"].ToString()),
                            TipoPagamento = dr["TipoPag"].ToString(),
                            Descricao = dr["Descricao"].ToString()
                        });
                        caixas.Add(int.Parse(dr["IDCaixa"].ToString()));
                    }
                }
                cmd.CommandText = "select Nome from caixas where IDCaixa = @caixa";
                for (int i = 0; i < mvm.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@caixa", caixas[i]);
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
        #region Movimentos
        #region Entradas
        [HttpGet]
        public async Task<IActionResult> Entrada()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (TempData["insertMsg"] != null) TempData.Keep("insertMsg");
            insert = true;
            List<string> nomesCaixas = new List<string>(), nomesDocs = new List<string>(), nomesPags = new List<string>(), nomesAtivs = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand("select IDCaixa, Nome from caixas where IDCaixa > 0", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync(); ;
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
            if (TempData["insert"] != null) TempData.Keep("insert");
            try
            {
                List<string> nomesCaixas = new List<string>(), nomesDocs = new List<string>(), nomesPags = new List<string>(), nomesAtivs = new List<string>();
                using (MySqlCommand cmd = new MySqlCommand("select IDCaixa, Nome from caixas where IDCaixa > 0", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync(); ;
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
                return await Task.Run(() => View("Entrada", model));
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a edição do registo: " + e.Message;
            }
            return await Task.Run(() => View("Index"));
        }
        [HttpPost]
        public async Task<IActionResult> Entrada(MovimentoViewModel mvm, int? id = null)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            try
            {
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
                mvm.TipoMovimento = "Entrada de tesouraria";
                if (mvm.DataHora == DateTime.MinValue) mvm.DataHora = DateTime.Now.Date;
                ModelState.Clear();
                TryValidateModel(mvm);
                if (ModelState.IsValid)
                {
                    using (MySqlCommand cmd = new MySqlCommand(insert ? "insert into movimentos(IDCaixa, Seccao, IDDocumento, TipoMovimento, User, DataHora, Valor, TipoPag, Descricao, Atividade) values (@caixa, @seccao, @documento, @tipomov, @user, @data, @valor, @tipopag, @descricao, @atividade);" : "update movimentos set IDCaixa = @caixa, Seccao = @seccao, IDDocumento = @documento, TipoMovimento = @tipomov, User = @user, DataHora = @data, Valor = @valor, TipoPag = @tipopag, Descricao = @descricao, Atividade = @atividade where IDMovimento = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                    {
                        if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                        if (id != null) cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@caixa", mvm.IDCaixa.Contains(" - ") ? mvm.IDCaixa.Substring(0, mvm.IDCaixa.IndexOf(" - ")) : mvm.IDCaixa);
                        cmd.Parameters.AddWithValue("@seccao", mvm.Seccao);
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
                    TempData["insertMsg"] = "Movimento gravado com sucesso";
                    return await Task.Run(() => Entrada());
                }
                return await Task.Run(() => Entrada((object)mvm));
            }
            catch (Exception e)
            {
                TempData["insertMsg"] = "Ocorreu um erro com a inserção do registo: " + e.Message;
            }
            return await Task.Run(() => View());
        }
        #endregion
        #region Saídas
        [HttpGet]
        public async Task<IActionResult> Saida()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (TempData["insertMsg"] != null) TempData.Keep("insertMsg");
            List<string> nomesCaixas = new List<string>(), nomesDocs = new List<string>(), nomesPags = new List<string>(), nomesAtivs = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand("select IDCaixa, Nome from caixas where IDCaixa > 0", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync(); ;
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
            if (TempData["insert"] != null) TempData.Keep("insert");
            List<string> nomesCaixas = new List<string>(), nomesDocs = new List<string>(), nomesPags = new List<string>(), nomesAtivs = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand("select IDCaixa, Nome from caixas where IDCaixa > 0", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync(); ;
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
            return await Task.Run(() => View("Saida", model));
        }
        [HttpPost]
        public async Task<IActionResult> Saida(MovimentoViewModel mvm, int? id = null)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            try
            {
                mvm.TipoMovimento = "Saida de tesouraria";
                mvm.User = await _userManager.GetUserAsync(User);
                if (mvm.DataHora == DateTime.MinValue) mvm.DataHora = DateTime.Now;
                ModelState.Clear();
                TryValidateModel(mvm);
                if (ModelState.IsValid)
                {
                    using (MySqlCommand cmd = new MySqlCommand(insert ? "insert into movimentos(IDCaixa, Seccao, IDDocumento, TipoMovimento, User, DataHora, Valor, TipoPag, Descricao, Atividade) values (@caixa, @seccao, @documento, @tipomov, @user, @data, @valor, @tipopag, @descricao, @atividade);" : "update movimentos set IDCaixa = @caixa, Seccao = @seccao, IDDocumento = @documento, TipoMovimento = @tipomov, User = @user, DataHora = @data, Valor = @valor, TipoPag = @tipopag, Descricao = @descricao, Atividade = @atividade where IDMovimento = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                    {
                        if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                        if (id != null) cmd.Parameters.AddWithValue("@id", id);
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
                        TempData["insertMsg"] = "Movimento gravado com sucesso";
                    }
                    return await Task.Run(() => Saida());
                }
                return await Task.Run(() => Saida((object)mvm));
            }
            catch (Exception e)
            {
                TempData["insertMsg"] = "Ocorreu um erro com a inserção do registo: " + e.Message;
            }
            return await Task.Run(() => View());
        }
        #endregion
        #region Transferências
        [HttpGet]
        public async Task<IActionResult> Transferencia()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (TempData["insertMsg"] != null) TempData.Keep("insertMsg");
            List<string> nomesCaixas = new List<string>(), nomesDocs = new List<string>(), nomesPags = new List<string>(), nomesAtivs = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand("select IDCaixa, Nome from caixas where IDCaixa > 0", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync(); ;
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
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync(); ;
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
            return await Task.Run(() => View("Transferencia", model));
        }
        [HttpPost]
        public async Task<IActionResult> Transferencia(MovimTransfViewModel mtvm)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            try
            {
                mtvm.User = await _userManager.GetUserAsync(User);
                if (mtvm.DataHora == DateTime.MinValue) mtvm.DataHora = DateTime.Now.Date;
                if (mtvm.IDCaixaOrigem == null)
                {
                    using (MySqlCommand cmd = new MySqlCommand("select IDCaixa from caixas where Grupo = @grupo", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                    {
                        if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
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
                        if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@caixa", mtvm.IDCaixaOrigem.Contains(" - ") ? mtvm.IDCaixaOrigem.Substring(0, mtvm.IDCaixaOrigem.IndexOf(" - ")) : mtvm.IDCaixaOrigem);
                        cmd.Parameters.AddWithValue("@documento", mtvm.IDDocumento.Substring(0, mtvm.IDDocumento.IndexOf(" - ")));
                        cmd.Parameters.AddWithValue("@tipomov", mtvm.TipoMovimento);
                        cmd.Parameters.AddWithValue("@user", mtvm.User.NormalizedUserName);
                        cmd.Parameters.AddWithValue("@data", mtvm.DataHora);
                        cmd.Parameters.AddWithValue("@valor", mtvm.Valor);
                        cmd.Parameters.AddWithValue("@tipopag", mtvm.TipoPagamento);
                        cmd.Parameters.AddWithValue("@descricao", mtvm.Descricao);
                        cmd.Parameters.AddWithValue("@atividade", mtvm.Atividade.Substring(0, mtvm.Atividade.IndexOf(" - ")));
                        await cmd.PrepareAsync();
                        await cmd.ExecuteNonQueryAsync();
                        cmd.Parameters["@tipomov"].Value = "Entrada de tesouraria";
                        cmd.Parameters["@caixa"].Value = mtvm.IDCaixaDestino.Substring(0, mtvm.IDCaixaDestino.IndexOf(" - "));
                        await cmd.PrepareAsync();
                        await cmd.ExecuteNonQueryAsync();
                        TempData["insertMsg"] = "Movimento gravado com sucesso";
                    }
                    return await Task.Run(() => Transferencia());
                }
                return await Task.Run(() => Transferencia((object)mtvm));
            }
            catch (Exception e)
            {
                TempData["insertMsg"] = "Ocorreu um erro com a inserção do registo: " + e.Message;
            }
            return await Task.Run(() => View());
        }
        #endregion
        public async Task<IActionResult> Editar(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            try
            {
                MovimentoViewModel mvm = new MovimentoViewModel();
                using (MySqlCommand cmd = new MySqlCommand("select movimentos.*, caixas.Nome as NomeCaixa, tipos_docs.Descricao as DescDoc, tipos_pags.Pagamento as DescPag, atividades.Nome as NomeAtiv from movimentos inner join caixas on movimentos.IDCaixa = caixas.IDCaixa inner join tipos_docs on movimentos.IDDocumento = tipos_docs.IDDocumento inner join tipos_pags on tipos_pags.IDPag = movimentos.TipoPag inner join atividades on atividades.IDAtividade = movimentos.Atividade where IDMovimento = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        if (!dr.HasRows) throw new Exception($"não foi encontrado nenhum registo com o ID \"{id}\"");
                        else
                        {
                            while (await dr.ReadAsync())
                            {
                                mvm.IDMovimento = int.Parse(dr["IDMovimento"].ToString());
                                mvm.IDCaixa = dr["IDCaixa"].ToString() + " - " + dr["NomeCaixa"].ToString();
                                mvm.IDDocumento = dr["IDDocumento"].ToString() + " - " + dr["DescDoc"].ToString();
                                mvm.Seccao = dr["Seccao"].ToString();
                                mvm.TipoMovimento = dr["TipoMovimento"].ToString();
                                mvm.User = await _userManager.FindByNameAsync(dr["User"].ToString());
                                mvm.DataHora = Convert.ToDateTime(dr["DataHora"].ToString());
                                mvm.Valor = decimal.Parse(dr["Valor"].ToString());
                                mvm.TipoPagamento = dr["TipoPag"].ToString() + " - " + dr["DescPag"].ToString();
                                mvm.Descricao = dr["Descricao"].ToString();
                                mvm.Atividade = dr["Atividade"].ToString() + " - " + dr["NomeAtiv"].ToString();
                            }
                        }
                    }
                }
                TempData["insert"] = false;
                return await Task.Run(() => mvm.TipoMovimento.Contains("Entrada") ? Entrada((object)mvm) : Saida((object)mvm));
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a edição do registo: " + e.Message;
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        #region Eliminar
        public async Task<IActionResult> Eliminar(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            try
            {
                MovimentoViewModel mvm = new MovimentoViewModel();
                using (MySqlCommand cmd = new MySqlCommand("select movimentos.*, caixas.Nome as NomeCaixa, tipos_docs.Descricao as DescDoc, tipos_pags.Pagamento as DescPag, atividades.Nome as NomeAtiv from movimentos inner join caixas on movimentos.IDCaixa = caixas.IDCaixa inner join tipos_docs on movimentos.IDDocumento = tipos_docs.IDDocumento inner join tipos_pags on tipos_pags.IDPag = movimentos.TipoPag inner join atividades on atividades.IDAtividade = movimentos.Atividade where IDMovimento = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        if (!dr.HasRows) throw new Exception($"não foi encontrado nenhum registo com o ID \"{id}\"");
                        else
                        {
                            while (await dr.ReadAsync())
                            {
                                mvm.IDMovimento = int.Parse(dr["IDMovimento"].ToString());
                                mvm.IDCaixa = dr["IDCaixa"].ToString() + " - " + dr["NomeCaixa"].ToString();
                                mvm.IDDocumento = dr["IDDocumento"].ToString() + " - " + dr["DescDoc"].ToString();
                                mvm.Seccao = dr["Seccao"].ToString();
                                mvm.TipoMovimento = dr["TipoMovimento"].ToString();
                                mvm.User = await _userManager.FindByNameAsync(dr["User"].ToString());
                                mvm.DataHora = Convert.ToDateTime(dr["DataHora"].ToString());
                                mvm.Valor = decimal.Parse(dr["Valor"].ToString());
                                mvm.TipoPagamento = dr["TipoPag"].ToString() + " - " + dr["DescPag"].ToString();
                                mvm.Descricao = dr["Descricao"].ToString();
                                mvm.Atividade = dr["Atividade"].ToString() + " - " + dr["NomeAtiv"].ToString();
                            }
                        }
                    }
                }
                return await Task.Run(() => View(mvm));
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a eliminação do registo: " + e.Message;
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        [HttpPost]
        public async Task<IActionResult> EliminarPost(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            try
            {
                MovimentoViewModel mvm = new MovimentoViewModel();
                using (MySqlCommand cmd = new MySqlCommand("delete from movimentos where IDMovimento = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.PrepareAsync();
                    int i = await cmd.ExecuteNonQueryAsync();
                    if (i == 0) throw new Exception($"não foi encontrado nenhum registo com o ID \"{id}\"");
                }
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a eliminação do registo: " + e.Message;
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        #endregion
        #endregion
        #region PDFs
        #region Diário de Caixa
        public async Task<IActionResult> DiarioCaixa()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<string> caixas = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand("select IDCaixa, Nome from caixas where " + (User.IsInRole("Administração de Agrupamento") ? "IDCaixa > 0;" : "Grupo = " + (await _userManager.GetUserAsync(User)).IDGrupo + (User.IsInRole("Equipa de Animação") ? " and Grupo in (select IDGrupo from grupos where Seccao = @seccao);" : ";")), new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                if (cmd.CommandText.Contains("@seccao")) cmd.Parameters.AddWithValue("@seccao", (await _userManager.GetUserAsync(User)).Seccao);
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) caixas.Add(dr["IDCaixa"].ToString() + " - " + dr["Nome"].ToString());
                }
            }
            ViewBag.caixas = caixas;
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<IActionResult> DiarioCaixa(string caixa, DateTime? inicio, DateTime? fim)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<MovimentoViewModel> listmvm = new List<MovimentoViewModel>();
            string _inicio = string.Format("{0}-{1}-{2}", inicio != null ? new string[] { inicio.Value.Year.ToString(), inicio.Value.Month.ToString(), inicio.Value.Day.ToString() } : new string[] { "100", "01", "01" }), _fim = string.Format("{0}-{1}-{2}", fim != null ? new string[] { fim.Value.Year.ToString(), fim.Value.Month.ToString() } : new string[] { DateTime.Today.Year.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Day.ToString() });
            using (MySqlCommand cmd = new MySqlCommand("select * from movimentos where IDMovimento > 0 and IDCaixa = @caixa and DataHora between @inicio and @fim;", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                cmd.Parameters.AddWithValue("@caixa", caixa.Substring(0, caixa.IndexOf(" - ")));
                cmd.Parameters.AddWithValue("@inicio", _inicio);
                cmd.Parameters.AddWithValue("@fim", _fim);
                await cmd.PrepareAsync();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    if (!dr.HasRows)
                    {
                        ModelState.AddModelError("", $"Não foram encontradas entradas para a caixa {caixa} entre as datas {(inicio == null ? DateTime.MinValue.ToString() : inicio.ToString())} e {(fim == null ? DateTime.MaxValue.ToString() : fim.ToString())}");
                        return await Task.Run(() => DiarioCaixa());
                    }
                    while (await dr.ReadAsync()) listmvm.Add(new MovimentoViewModel()
                    {
                        IDMovimento = int.Parse(dr["IDMovimento"].ToString()),
                        IDCaixa = dr["IDCaixa"].ToString(),
                        IDDocumento = dr["IDDocumento"].ToString(),
                        TipoMovimento = dr["TipoMovimento"].ToString(),
                        User = await _userManager.FindByNameAsync(dr["User"].ToString()),
                        DataHora = Convert.ToDateTime(dr["DataHora"].ToString()),
                        Valor = decimal.Parse(dr["Valor"].ToString()),
                        TipoPagamento = dr["TipoPag"].ToString(),
                        Descricao = dr["Descricao"].ToString(),
                        Atividade = dr["Atividade"].ToString()
                    });
                }
            }
            ViewData["TituloAnalise"] = "Diário de caixa";
            string parametros = $"caixa {caixa}, ";
            if (inicio != null)
            {
                if (inicio == fim) parametros += "só a data " + inicio.Value.Date;
                else parametros += $"desde a data {inicio.Value.Date} ";
            }
            else parametros += "do movimento mais antigo ";
            if (fim != null)
            {
                if (inicio != fim) parametros += $"até à data {fim}";
            }
            else parametros += "até ao movimento mais recente";
            ViewData["parametros"] = parametros;
            return await Task.Run(() => new ViewAsPdf("Analises/MovimentosCaixa", listmvm, viewData: ViewData)
            {
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(5, 5, 5, 5),
                PageSize = Size.A4
            });
        }
        #endregion
        #region Movimentos entre parâmetros
        public async Task<IActionResult> MovParam()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            ViewBag.caixas = listcaixas;
            ViewBag.pags = listpags;
            ViewBag.ativs = listativs;
            ViewBag.seccoes = listseccoes;
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<IActionResult> MovParam(DateTime? dataInicio = null, DateTime? dataFim = null, string caixaInicio = null, string caixaFim = null, string tipoPagInicio = null, string tipoPagFim = null, string ativInicio = null, string ativFim = null, string seccaoInicio = null, string seccaoFim = null, bool detalhado = false)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<MovimentoViewModel> mvm = new List<MovimentoViewModel>();
            if (tipoPagInicio == null || tipoPagFim == null)
            {
                ModelState.AddModelError("", "Há pelo menos um tipo de pagamento em falta.");
                return await Task.Run(() => View());
            }
            else if ((caixaInicio == null && caixaFim != null) || (caixaInicio != null && caixaFim == null))
            {
                ModelState.AddModelError("", "Há uma caixa em falta.");
                return await Task.Run(() => View());
            }
            else if ((ativInicio == null && ativFim != null) || (ativInicio != null && ativFim == null))
            {
                ModelState.AddModelError("", "Há uma atividade em falta.");
                return await Task.Run(() => View());
            }
            else if ((seccaoInicio == null && seccaoFim != null) || (seccaoInicio != null && seccaoFim == null))
            {
                ModelState.AddModelError("", "Há uma seccão em falta.");
                return await Task.Run(() => View());
            }
            if (tipoPagInicio[0] > tipoPagFim[0])
            {
                string temp = tipoPagFim;
                tipoPagFim = tipoPagInicio;
                tipoPagInicio = temp;
            }
            if (seccaoInicio[0] > seccaoFim[0])
            {
                string temp = seccaoFim;
                seccaoFim = seccaoInicio;
                seccaoInicio = temp;
            }
            if (int.Parse(ativInicio.Substring(0, ativInicio.IndexOf(" - "))) > int.Parse(ativFim.Substring(0, ativFim.IndexOf(" - "))))
            {
                string temp = ativFim;
                ativFim = ativInicio;
                ativInicio = temp;
            }
            if (dataInicio > dataFim)
            {
                DateTime temp = (DateTime)dataFim;
                dataFim = dataInicio;
                dataInicio = temp;
            }
            if (int.Parse(caixaInicio.Substring(0, caixaInicio.IndexOf(" - "))) > int.Parse(caixaFim.Substring(0, caixaFim.IndexOf(" - "))))
            {
                string temp = caixaFim;
                caixaFim = caixaInicio;
                caixaInicio = temp;
            }
            string datas = "";
            if (dataInicio == dataFim) datas = string.Format("(cast(DataHora as date) between '{0}' and '{1}')", dataInicio == null ? $"1900-{DateTime.MinValue.Month}-{DateTime.MinValue.Day}" : $"{Convert.ToDateTime(dataInicio).Year}-{Convert.ToDateTime(dataInicio).Month}-{Convert.ToDateTime(dataInicio).Day}", dataInicio == null ? $"{DateTime.MaxValue.Year}-{DateTime.MaxValue.Month}-{DateTime.MaxValue.Day}" : $"{Convert.ToDateTime(dataInicio).Year}-{Convert.ToDateTime(dataInicio).Month}-{Convert.ToDateTime(dataInicio).Day}");
            else datas = string.Format("cast(DataHora as date) between '{0}' and '{1}'", dataInicio == null ? $"1900-{DateTime.MinValue.Month}-{DateTime.MinValue.Day}" : $"{Convert.ToDateTime(dataInicio).Year}-{Convert.ToDateTime(dataInicio).Month}-{Convert.ToDateTime(dataInicio).Day}", dataFim == null ? $"{DateTime.MaxValue.Year}-{DateTime.MaxValue.Month}-{DateTime.MaxValue.Day}" : $"{Convert.ToDateTime(dataFim).Year}-{Convert.ToDateTime(dataFim).Month}-{Convert.ToDateTime(dataFim).Day}");
            string pags = "";
            if (tipoPagInicio == tipoPagFim) pags = string.Format("TipoPag like '{0}'", tipoPagInicio.Substring(0, tipoPagInicio.IndexOf(" - ")));
            else pags = string.Format("TipoPag between '{0}' and '{1}'", tipoPagInicio != null ? tipoPagInicio.Substring(0, tipoPagInicio.IndexOf(" - ")) : listpags[0].Substring(0, listpags[0].IndexOf(" - ")), tipoPagFim == null ? tipoPagFim.Substring(0, tipoPagFim.IndexOf(" - ")) : listpags[^1].Substring(0, listpags[^1].IndexOf(" - ")));
            string ativs = "";
            if (ativInicio == ativFim) ativs = string.Format("Atividade = {0}", ativInicio.Substring(0, ativFim.IndexOf(" - ")));
            else ativs = string.Format("Atividade between {0} and {1}", ativInicio != null ? ativInicio.Substring(0, ativInicio.IndexOf(" - ")) : listativs[0].Substring(0, listativs[0].IndexOf(" - ")), ativFim != null ? ativFim.Substring(0, ativFim.IndexOf(" - ")) : listativs[^1].Substring(0, this.listativs[^1].IndexOf(" - ")));
            string seccoes = "";
            if (seccaoInicio == seccaoFim) seccoes = string.Format("Seccao like '{0}'", seccaoInicio ?? "Agrupamento");
            //else if (seccaoInicio == "Agrupamento" || seccaoFim == "Agrupamento") seccoes = string.Format("Seccao like '{0}'", seccaoInicio ?? "Agrupamento");
            else seccoes = string.Format("Seccao between '{0}' and '{1}'", seccaoInicio ?? "Agrupamento", seccaoFim ?? "Agrupamento");
            string caixas = "";
            if (caixaInicio == caixaFim) caixas = string.Format("IDCaixa = {0}", caixaInicio.Substring(0, ativFim.IndexOf(" - ")));
            else caixas = string.Format("IDCaixa between {0} and {1}", caixaInicio == null ? listcaixas[0] : caixaInicio.Substring(0, caixaInicio.IndexOf(" - ")), caixaFim == null ? listcaixas[^1] : caixaFim.Substring(0, caixaFim.IndexOf(" - ")));
            using (MySqlCommand cmd = new MySqlCommand("select * from movimentos" + (dataInicio == null && dataFim == null && caixaInicio == null && caixaFim == null && tipoPagInicio == null && tipoPagFim == null && ativInicio == null && ativFim == null && seccaoInicio == null && seccaoFim == null ? "" : " where @datas and @pags and @ativs and @seccoes and @caixas;"), new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                await cmd.PrepareAsync();
                cmd.CommandText = cmd.CommandText.Replace("@datas", datas).Replace("@ativs", ativs).Replace("@pags", pags).Replace("@seccoes", seccoes).Replace("@caixas", caixas);
                await cmd.PrepareAsync();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) mvm.Add(new MovimentoViewModel()
                    {
                        IDCaixa = dr["IDCaixa"].ToString(),
                        IDDocumento = dr["IDDocumento"].ToString(),
                        Seccao = dr["Seccao"].ToString(),
                        TipoMovimento = dr["TipoMovimento"].ToString(),
                        User = await _userManager.FindByNameAsync(dr["User"].ToString()),
                        DataHora = Convert.ToDateTime(dr["DataHora"].ToString()),
                        Valor = decimal.Parse(dr["Valor"].ToString()),
                        TipoPagamento = dr["TipoPag"].ToString(),
                        Descricao = dr["Descricao"].ToString(),
                        Atividade = dr["Atividade"].ToString()
                    });
                }
            }
            string parametros = "";
            if (dataInicio != null)
            {
                if (dataInicio == dataFim) parametros += "só em " + dataInicio;
                else parametros += "entre " + Convert.ToDateTime(dataInicio).Date;
            }
            else parametros += "do mais antigo";
            if (dataFim != null && dataInicio != dataFim) parametros += " e " + Convert.ToDateTime(dataFim).Date + ", ";
            else if (dataFim == null) parametros += " ao mais recente, ";
            else parametros += ", ";
            if (caixaInicio != null)
            {
                if (caixaInicio == listcaixas[0] && caixaFim == listcaixas[^1]) parametros += "todas as caixas";
                else if (caixaInicio == caixaFim) parametros += "só a caixa " + caixaInicio;
                else parametros += "da caixa " + caixaInicio;
            }
            else parametros += "todas as caixas";
            if (caixaFim != null && caixaInicio != caixaFim && !(caixaInicio == listcaixas[0] && caixaFim == listcaixas[^1])) parametros += " até à caixa " + caixaFim + ", ";
            else parametros += ", ";
            if (tipoPagInicio != null)
            {
                if (tipoPagInicio == listpags[0] && tipoPagFim == listpags[^1]) parametros += "todos os tipos de pagamento";
                else
                {
                    if (tipoPagInicio != tipoPagFim) parametros += "entre o tipo de pagamento " + tipoPagInicio;
                    else if (tipoPagInicio == tipoPagFim) parametros += "só o tipo de pagamento " + tipoPagInicio;
                }
            }
            else parametros += "todos os pagamentos";
            if (tipoPagFim != null && tipoPagInicio != tipoPagFim && !(tipoPagInicio == listpags[0] && tipoPagFim == listpags[^1])) parametros += " e o pagamento " + tipoPagFim + ", ";
            else parametros += ", ";
            if (ativInicio != null)
            {
                if (ativInicio == listativs[0] && ativFim == listativs[^1]) parametros += "todas as atividades";
                else if (ativInicio == ativFim) parametros += "só a atividade " + ativInicio;
                else parametros += "da atividade " + ativInicio;
            }
            else parametros += "todas as atividades";
            if (ativFim != null && ativInicio != ativFim && (ativInicio != listativs[0] && ativFim != listativs[^1])) parametros += " até a atividade " + ativFim + ", ";
            else parametros += ", ";
            if (seccaoInicio != null)
            {
                if ((seccaoInicio == "Agrupamento" || seccaoFim == "Agrupamento") || (seccaoInicio == "Lobitos" && seccaoFim == "Agrupamento")) parametros += "todo o agrupamento";
                else if (seccaoInicio != seccaoFim) parametros += "entre os " + seccaoInicio;
                else if (seccaoInicio == seccaoFim) parametros += "só os " + seccaoInicio;
            }
            else parametros += "todo o agrupamento";
            if (seccaoFim != null && seccaoInicio != seccaoFim && !(seccaoInicio == "Lobitos" && seccaoFim == "Agrupamento")) parametros += " e os " + seccaoFim;
            ViewData["parametro"] = parametros;
            ViewData["detalhado"] = detalhado;
            ViewData["TituloAnalise"] = "Movimentos entre parâmetros";
            return await Task.Run(() => new ViewAsPdf("Analises/MovimentosCaixa", mvm, viewData: ViewData)
            {
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(5, 5, 5, 5),
                PageSize = Size.A4
            });
        }
        #endregion
        #region Rankings
        public async Task<IActionResult> Rankings()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            ViewBag.ativs = listativs;
            List<string> grupos = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand("select IDGrupo, Nome from grupos where IDGrupo > 0", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                if (User.IsInRole("Comum"))
                {
                    cmd.CommandText += " where IDGrupo = @id";
                    cmd.Parameters.AddWithValue("@id", (await _userManager.GetUserAsync(User)).IDGrupo);
                }
                else if (User.IsInRole("Equipa de Animação"))
                {
                    cmd.CommandText += " where Seccao = @seccao";
                    cmd.Parameters.AddWithValue("@seccao", (await _userManager.GetUserAsync(User)).Seccao);
                }
                await cmd.PrepareAsync();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) grupos.Add(dr["IDGrupo"].ToString() + " - " + dr["Nome"].ToString());
                }
            }
            grupos.Sort();
            ViewBag.grupos = grupos.OrderBy(x => x.Substring(0, x.IndexOf(" - ")).Length).ToList();
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<IActionResult> RankingsAtivs(string ativInicio = null, string ativFim = null, string ordem = null)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<MovimentoViewModel> mvm = new List<MovimentoViewModel>();
            using (MySqlCommand cmd = new MySqlCommand("select movimentos.*, atividades.Nome from movimentos inner join atividades on movimentos.Atividade = atividades.IDAtividade where movimentos.IDMovimento > 0", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                if (ativInicio != null || ativFim != null)
                {
                    if (int.Parse(ativInicio.Substring(0, ativInicio.IndexOf(" - "))) > int.Parse(ativFim.Substring(0, ativFim.IndexOf(" - "))))
                    {
                        string temp = ativFim;
                        ativFim = ativInicio;
                        ativInicio = temp;
                    }
                    cmd.CommandText += (ativInicio == ativFim ? " and movimentos.Atividade = @inicio" : " and movimentos.Atividade between @inicio and @fim");
                    if (ativInicio == null) cmd.CommandText = cmd.CommandText.Replace("@inicio", "(select (min(IDAtividade) from atividades)");
                    else cmd.Parameters.AddWithValue("@inicio", ativInicio.Substring(0, ativInicio.IndexOf(" - ")));
                    if (ativFim != ativInicio)
                    {
                        if (ativFim == null) cmd.CommandText = cmd.CommandText.Replace("@fim", "(select (max(IDAtividade) from atividades)");
                        else cmd.Parameters.AddWithValue("@fim", ativFim.Substring(0, ativFim.IndexOf(" - ")));
                    }
                }
                if (ordem != null) cmd.CommandText += " order by movimentos.Valor" + (ordem == "Ascendente" ? " asc" : " desc");
                if (cmd.CommandText.Contains("@inicio")) await cmd.PrepareAsync();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        mvm.Add(new MovimentoViewModel()
                        {
                            IDCaixa = dr["IDCaixa"].ToString(),
                            IDDocumento = dr["IDDocumento"].ToString(),
                            Seccao = dr["Seccao"].ToString(),
                            TipoMovimento = dr["TipoMovimento"].ToString(),
                            User = await _userManager.FindByNameAsync(dr["User"].ToString()),
                            DataHora = Convert.ToDateTime(dr["DataHora"].ToString()),
                            Valor = decimal.Parse(dr["Valor"].ToString()),
                            TipoPagamento = dr["TipoPag"].ToString(),
                            Descricao = dr["Descricao"].ToString(),
                            Atividade = dr["Atividade"].ToString() + " - " + dr["Nome"].ToString()
                        });
                    }
                }
            }
            string parametros = "";
            if (ativInicio != null)
            {
                if (ativInicio == ativFim) parametros += "só a atividade " + ativInicio;
                else if (ativInicio == listativs[0] && ativFim == listativs[^1]) parametros += "todas as atividades";
                else parametros += "entre a atividade " + ativInicio;
            }
            else parametros += "todas as atividades";
            if (ativFim != null && ativInicio != ativFim && (ativInicio != listativs[0] && ativFim != listativs[^1])) parametros += " até a atividade " + ativFim + ", ";
            else parametros += ", ";
            ViewData["parametros"] = parametros + "ordem " + (ordem == null ? "ascendente" : char.ToLower(ordem[0]) + ordem.Substring(1));
            ViewData["TituloAnalise"] = "Rankings entre atividades";
            return await Task.Run(() => new ViewAsPdf("Analises/RankingsAtivs", mvm, viewData: ViewData)
            {
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(5, 5, 5, 5),
                PageSize = Size.A4
            });
        }
        [HttpPost]
        public async Task<IActionResult> RankingsGrupos(string grupoInicio = null, string grupoFim = null, string ordem = null)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<MovimentoViewModel> mvm = new List<MovimentoViewModel>();
            List<string> grupos = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand("select movimentos.*, caixas.Nome from movimentos inner join caixas on movimentos.IDCaixa = caixas.IDCaixa where movimentos.IDMovimento > 0", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();

                if (grupoInicio != null || grupoFim != null)
                {
                    if (int.Parse(grupoInicio.Substring(0, grupoInicio.IndexOf(" - "))) > int.Parse(grupoFim.Substring(0, grupoFim.IndexOf(" - "))))
                    {
                        string temp = grupoFim;
                        grupoFim = grupoInicio;
                        grupoInicio = temp;
                    }
                    cmd.CommandText += (grupoInicio == grupoFim ? " and caixas.IDCaixa = (select IDCaixa from caixas where Grupo = @inicio)" : " and caixas.IDCaixa between (select IDCaixa from caixas where Grupo = @inicio) and (select IDCaixa from caixas where Grupo = @fim)");
                    if (grupoInicio == null) cmd.CommandText = cmd.CommandText.Replace("@inicio", "(select (min(IDCaixa) from caixas)");
                    else cmd.Parameters.AddWithValue("@inicio", grupoInicio.Substring(0, grupoInicio.IndexOf(" - ")));
                    if (grupoFim != grupoInicio)
                    {
                        if (grupoFim == null) cmd.CommandText = cmd.CommandText.Replace("@fim", "(select (max(IDCaixa) from caixas)");
                        else cmd.Parameters.AddWithValue("@fim", grupoFim.Substring(0, grupoFim.IndexOf(" - ")));
                    }
                }
                if (ordem != null) cmd.CommandText += " order by movimentos.Valor" + (ordem == "Ascendente" ? " asc" : " desc");
                if (cmd.CommandText.Contains("@inicio")) await cmd.PrepareAsync();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        mvm.Add(new MovimentoViewModel()
                        {
                            IDCaixa = dr["IDCaixa"].ToString() + " - " + dr["Nome"].ToString(),
                            IDDocumento = dr["IDDocumento"].ToString(),
                            Seccao = dr["Seccao"].ToString(),
                            TipoMovimento = dr["TipoMovimento"].ToString(),
                            User = await _userManager.FindByNameAsync(dr["User"].ToString()),
                            DataHora = Convert.ToDateTime(dr["DataHora"].ToString()),
                            Valor = decimal.Parse(dr["Valor"].ToString()),
                            TipoPagamento = dr["TipoPag"].ToString(),
                            Descricao = dr["Descricao"].ToString(),
                            Atividade = dr["Atividade"].ToString()
                        });
                    }
                }
                cmd.CommandText = "select IDGrupo, Nome from grupos where IDGrupo > 0";
                cmd.Parameters.Clear();
                await cmd.PrepareAsync();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) grupos.Add(dr["IDGrupo"].ToString() + " - " + dr["Nome"].ToString());
                }
            }
            string parametros = "";
            if (grupoInicio != null)
            {
                if (grupoInicio == grupoFim) parametros += "só a caixa com o grupo " + grupoInicio;
                else if (grupoInicio == grupos[0] && grupoFim == grupos[^1]) parametros += "todas as caixas";
                else parametros += "entre a caixa com o grupo " + grupoInicio;
            }
            if (grupoFim != null && grupoInicio != grupoFim && (grupoInicio != listativs[0] && grupoFim != listativs[^1])) parametros += " até à caixa com o grupo " + grupoFim + ", ";
            else parametros += ", ";
            ViewData["parametros"] = parametros + "ordem " + (ordem == null ? "ascendente" : char.ToLower(ordem[0]) + ordem.Substring(1));
            ViewData["TituloAnalise"] = "Rankings entre grupos";
            return await Task.Run(() => new ViewAsPdf("Analises/RankingsGrupos", mvm, viewData: ViewData)
            {
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(5, 5, 5, 5),
                PageSize = Size.A4
            });
        }
        #endregion
        #region Movimentos contra o orçamento de uma atividade
        public async Task<IActionResult> MovOrcam()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (TempData["errAtiv"] != null) TempData.Keep("errAtiv");
            ViewBag.ativs = listativs;
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<IActionResult> MovOrcam(string ativ = null)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            List<MovimentoViewModel> listmvm = new List<MovimentoViewModel>();
            if (ativ != null)
            {
                ViewData["atividade"] = ativ;
                using (MySqlCommand cmd = new MySqlCommand("select movimentos.*, caixas.Nome as NomeCaixa, tipos_docs.Descricao as DescDoc, tipos_pags.Pagamento as DescPag from movimentos inner join caixas on movimentos.IDCaixa = caixas.IDCaixa inner join tipos_docs on movimentos.IDDocumento = tipos_docs.IDDocumento inner join tipos_pags on movimentos.TipoPag = tipos_pags.IDPag where movimentos.Atividade = @ativ", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@ativ", ativ.Substring(0, ativ.IndexOf(" - ")));
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) listmvm.Add(new MovimentoViewModel()
                        {
                            IDMovimento = int.Parse(dr["IDMovimento"].ToString()),
                            IDCaixa = dr["IDCaixa"].ToString() + " - " + dr["NomeCaixa"].ToString(),
                            IDDocumento = dr["IDDocumento"].ToString() + " - " + dr["DescDoc"].ToString(),
                            Seccao = dr["Seccao"].ToString(),
                            TipoMovimento = dr["TipoMovimento"].ToString(),
                            User = await _userManager.FindByNameAsync(dr["User"].ToString()),
                            DataHora = Convert.ToDateTime(dr["DataHora"].ToString()),
                            Valor = decimal.Parse(dr["Valor"].ToString()),
                            TipoPagamento = dr["TipoPag"].ToString() + " - " + dr["DescPag"].ToString(),
                            Descricao = dr["Descricao"].ToString()
                        });
                    }
                    cmd.CommandText = "select Orcamento from atividades where IDAtividade = @ativ";
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) ViewData["orcamento"] = dr["Orcamento"].ToString();
                    }
                    cmd.CommandText = "select count(IDParticipante) from participantes where IDAtividade = @ativ";
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) ViewData["countpartip"] = dr["count(IDParticipante)"].ToString();
                    }
                }
                ViewData["TituloAnalise"] = "Movimentos contra o orçamento";
                return await Task.Run(() => new ViewAsPdf("Analises/MovOrcam", listmvm, viewData: ViewData)
                {
                    PageOrientation = Orientation.Landscape,
                    PageMargins = new Margins(5, 5, 5, 5),
                    PageSize = Size.A4
                });
            }
            TempData["errAtiv"] = "A atividade não pode estar vazia";
            return await Task.Run(() => RedirectToAction("MovOrcam"));
        }
        #endregion
        #endregion
    }
}