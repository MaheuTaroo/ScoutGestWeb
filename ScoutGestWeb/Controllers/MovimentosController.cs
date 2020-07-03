﻿using Microsoft.AspNetCore.Identity;
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
    public class MovimentosController : Controller
    {
        #region Variáveis 
        private readonly List<MovimentoViewModel> mvm = new List<MovimentoViewModel>();
        private readonly List<int> caixas = new List<int>();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly List<string> listcaixas = new List<string>(), listpags = new List<string>(), listativs = new List<string>(), listseccoes = new List<string>();
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
            string _inicio = string.Format("{0}-{1}-{2} {3}:{4}:{5}", inicio != null ? new string[] { inicio.Value.Year.ToString(), inicio.Value.Month.ToString(), inicio.Value.Day.ToString(), inicio.Value.Hour.ToString(), inicio.Value.Minute.ToString(), inicio.Value.Second.ToString() } : new string[] { DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString(), "00", "00", "00" }), _fim = string.Format("{0}-{1}-{2} {3}:{4}:{5}", fim != null ? new string[] { fim.Value.Year.ToString(), fim.Value.Month.ToString(), fim.Value.Day.ToString(), fim.Value.Hour.ToString(), fim.Value.Minute.ToString(), fim.Value.Second.ToString() } : new string[] { DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString(), "23", "59", "59" });
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
            return await Task.Run(() => new ViewAsPdf("Analises/MovimentosCaixa", listmvm)
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
        public async Task<IActionResult> MovParam(DateTime? dataInicio = null, DateTime? dataFim = null, string caixaInicio = null, string caixaFim = null, string tipoPagInicio = null, string tipoPagFim = null, string ativInicio = null, string ativFim = null, string seccaoInicio = null, string seccaoFim = null)
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
                if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
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
            ViewData["parametros"] = parametros;
            return await Task.Run(() => new ViewAsPdf("Analises/MovimentosCaixa", mvm, viewData: ViewData)
            {
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(5, 5, 5, 5),
                PageSize = Size.A4
            });
        }
        #endregion
        public async Task<IActionResult> PDFCaixas(string caixaInicio, string caixaFim)
        {
            List<CaixaViewModel> cvm = new List<CaixaViewModel>();
            List<string> grupos = new List<string>(), escuteiros = new List<string>();
            List<int> gruposNum = new List<int>(), escutNum = new List<int>();
            using (MySqlCommand cmd = new MySqlCommand("select * from caixas where IDCaixa between @inicio and @fim", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                cmd.Parameters.AddWithValue("@inicio", caixaInicio.Substring(0, caixaInicio.IndexOf(" - ")));
                cmd.Parameters.AddWithValue("@fim", caixaFim.Substring(0, caixaFim.IndexOf(" - ")));
                await cmd.PrepareAsync();
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
                        })) grupos.Add(dr["Nome"].ToString());
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
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(5, 5, 5, 5),
                PageSize = Size.A4
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
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(5, 5, 5, 5)
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