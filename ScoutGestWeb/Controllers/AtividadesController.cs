using Microsoft.AspNetCore.Identity;
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
    public class AtividadesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        bool insert = true;
        public AtividadesController(UserManager<ApplicationUser> userManager) => _userManager = userManager;
        public async Task<IActionResult> Index(string coluna, string procura)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (TempData["msg"] != null) TempData["msgKeep"] = TempData["msg"];
            List<AtividadeViewModel> avm = new List<AtividadeViewModel>();
            if (string.IsNullOrEmpty(coluna) && string.IsNullOrEmpty(procura))
            {
                using (MySqlCommand cmd = new MySqlCommand("select IDAtividade, Nome, DataInicio, DataFim from atividades where Ativa = 1" + (User.IsInRole("Equipa de Animação") || User.IsInRole("Comum") ? " and Seccao = @seccao or Seccao = \"Agrupamento\"" + (User.IsInRole("Comum") ? " and DataInicio between now() - interval 3 day and now() + interval 3 day" : "") : "") + (User.IsInRole("Comum") ? ";" : " order by DataInicio desc limit 25;"), new MySqlConnection("server =localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                    if (User.IsInRole("Equipa de Animação") || User.IsInRole("Comum"))
                    {
                        cmd.Parameters.AddWithValue("@seccao", (await _userManager.GetUserAsync(User)).Seccao);
                    }
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            avm.Add(new AtividadeViewModel()
                            {
                                IDAtividade = int.Parse(dr["IDAtividade"].ToString()),
                                Nome = dr["Nome"].ToString(),
                                DataInicio = DateTime.Parse(dr["DataInicio"].ToString()),
                                DataFim = DateTime.Parse(dr["DataFim"].ToString())
                            });
                        }
                    }
                    cmd.Connection.Close();
                }
            }
            else
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand("select IDAtividade, Nome, DataInicio, DataFim from atividades where pesquisa" + (User.IsInRole("Equipa de Animação") || User.IsInRole("Comum") ? " and Seccao = @seccao or Seccao = \"Agrupamento\"" + (User.IsInRole("Comum") ? " and DataInicio between now() - interval 3 day and now() + interval 3 day" : "") : "") + (User.IsInRole("Comum") ? ";" : " order by DataInicio desc limit 25;"), new MySqlConnection("server =localhost; port=3306; database=scoutgest; user=root")))
                    {
                        if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                        if (User.IsInRole("Equipa de Animação") || User.IsInRole("Comum"))
                        {
                            cmd.Parameters.AddWithValue("@seccao", (await _userManager.GetUserAsync(User)).Seccao);
                        }
                        switch (coluna)
                        {
                            case "Nome":
                            case "Tipo":
                            case "Tema":
                            case "Local":
                                cmd.CommandText = cmd.CommandText.Replace("pesquisa", coluna + " like '%" + procura + "%'"); ;
                                break;
                            case "Data inicial":
                            case "Data final":
                                cmd.CommandText = cmd.CommandText.Replace("pesquisa", "Data" + (coluna.Contains("inicial") ? "Inicio" : "Fim") + " like " + Convert.ToDateTime(procura).ToString("yyyy-MM-dd"));
                                break;
                            case "Orçamento":
                                cmd.CommandText = cmd.CommandText.Replace("pesquisa", "Orcamento = " + double.Parse(procura));
                                break;
                            case "Aberta a movimentos":
                                cmd.CommandText = cmd.CommandText.Replace("pesquisa", "Ativa = " + bool.Parse(procura));
                                break;
                        }
                        cmd.Connection.Close();
                    }
                }
                catch (Exception e)
                {
                    if (e.Message.Contains("was not recognized as a valid DateTime.")) ModelState.AddModelError("", "O texto introduzido não foi reconhecido como uma data válida");
                    if (e.Message.Contains("")) ModelState.AddModelError("", e.Message);
                    return await Task.Run(() => Index(null, null));
                }
            }
            return await Task.Run(() => View(avm));
        }
        #region Nova atividade
        [HttpGet]
        public async Task<IActionResult> InserirAtividade()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (!User.IsInRole("Administração de Agrupamento")) return Forbid();
            if (TempData["insertMsg"] != null) TempData["insertMsgKeep"] = TempData["insertMsg"];
            insert = true;
            return await Task.Run(() => View());
        }
        public async Task<IActionResult> InserirAtividade(object model)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (!User.IsInRole("Administração de Agrupamento")) return Forbid();
            insert = false;
            return await Task.Run(() => View("InserirAtividade", model));
        }
        [HttpPost]
        public async Task<IActionResult> InserirAtividade(AtividadeViewModel avm)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (!User.IsInRole("Administração de Agrupamento")) return Forbid();
            if (ModelState.IsValid)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(insert ? "insert into atividades(Nome, Tipo, Tema, Seccao, Local, DataInicio, DataFim, Orcamento, Ativa) values (@nome, @tipo, @tema, @seccao, @local, @inicio, @fim, @orcamento, @ativa);" : "update atividades set Nome = @nome, Tipo = @tipo, Tema = @tema, Seccao = @seccao, Local = @local, DataInicio = @dataInicio, DataFim = @dataFim, Orcamento = @orcamento, Ativa = @ativa where IDAtividade = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                    {
                        if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@nome", avm.Nome);
                        cmd.Parameters.AddWithValue("@tipo", avm.Tipo);
                        cmd.Parameters.AddWithValue("@tema", avm.Tema);
                        cmd.Parameters.AddWithValue("@seccao", avm.Seccao);
                        cmd.Parameters.AddWithValue("@local", avm.Local);
                        cmd.Parameters.AddWithValue("@inicio", avm.DataInicio);
                        cmd.Parameters.AddWithValue("@fim", avm.DataFim);
                        cmd.Parameters.AddWithValue("@orcamento", avm.Orcamento);
                        cmd.Parameters.AddWithValue("@ativa", avm.Ativa == true ? 1 : 0);
                        await cmd.PrepareAsync();
                        await cmd.ExecuteNonQueryAsync();
                        avm.Participantes = avm.Participantes == null ? "" : avm.Participantes.Trim();
                        avm.RecFinanceiros = avm.RecFinanceiros == null? "" : avm.RecFinanceiros.Trim();
                        avm.RecHumanos = avm.RecHumanos == null ? "" : avm.RecHumanos.Trim();
                        avm.RecMateriais = avm.RecMateriais == null ? "" : avm.RecMateriais.Trim();
                        if (!string.IsNullOrEmpty(avm.Participantes))
                        {
                            string[] participantes = avm.Participantes.Split("\r\n");
                            cmd.CommandText = insert ? "insert into participantes values " : "delete from participantes where IDAtividade = @id; insert into participantes values ";
                            for (int i = 0; i < participantes.Length; i++)
                            {
                                cmd.CommandText += $"(@id, @particip{i + 1}), ";
                                cmd.Parameters.AddWithValue($"@particip{i + 1}", participantes[i]);
                            }
                            cmd.CommandText = cmd.CommandText.Remove(cmd.CommandText.LastIndexOf(", "));
                            await cmd.PrepareAsync();
                            await cmd.ExecuteNonQueryAsync();
                        }
                        if (!(string.IsNullOrEmpty(avm.RecFinanceiros) && string.IsNullOrEmpty(avm.RecHumanos) && string.IsNullOrEmpty(avm.RecMateriais)))
                        {
                            cmd.CommandText = insert ? "insert into recursos values (@id, @human, @mater, @financ" : "update recursos set RecHumanos = @human, RecMateriais = @mater, RecFinanceiros = @financ where IDAtividade = @id";
                            cmd.Parameters.AddWithValue("@human", avm.RecHumanos);
                            cmd.Parameters.AddWithValue("@mater", avm.RecMateriais);
                            cmd.Parameters.AddWithValue("@financ", avm.RecFinanceiros);
                            await cmd.PrepareAsync();
                            await cmd.ExecuteNonQueryAsync();
                        }
                        cmd.Connection.Close();
                    }
                    TempData["insertMsg"] = "Atividade inserida com sucesso";
                    return await Task.Run(() => RedirectToAction("InserirEscuteiro"));
                }
                catch (MySqlException mse)
                {
                    TempData["insertMsg"] = "Erro na inserção na base de dados: " + mse.Message;
                    return await Task.Run(() => View());
                }
                catch (Exception e)
                {
                    TempData["insertMsg"] = "Ocorreu um erro: " + e.Message;
                    return await Task.Run(() => View());
                }
            }
            return await Task.Run(() => RedirectToAction("InserirAtividade", (object)avm));
        }
        #endregion
        public async Task<IActionResult> Editar(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (!User.IsInRole("Administração de Agrupamento")) return Forbid();
            try
            {
                AtividadeViewModel avm = new AtividadeViewModel();
                using (MySqlCommand cmd = new MySqlCommand("select * from atividades where IDAtividade = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
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
                            while (await dr.ReadAsync())
                            {
                                avm.IDAtividade = int.Parse(dr["IDAtividade"].ToString());
                                avm.Nome = dr["Nome"].ToString();
                                avm.Tipo = dr["Tipo"].ToString();
                                avm.Tema = dr["Tema"].ToString();
                                avm.Seccao = dr["Seccao"].ToString();
                                avm.Local = dr["Local"].ToString();
                                avm.DataInicio = Convert.ToDateTime(dr["DataInicio"].ToString());
                                avm.DataFim = Convert.ToDateTime(dr["DataFim"].ToString());
                                avm.Orcamento = decimal.Parse(dr["Orcamento"].ToString());
                                avm.Ativa = bool.Parse(dr["Ativa"].ToString());
                            }
                        }
                    }
                    cmd.CommandText = cmd.CommandText.Replace("atividades", "recursos");
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            avm.RecFinanceiros = dr["RecFinanceiros"].ToString();
                            avm.RecHumanos = dr["RecHumanos"].ToString();
                            avm.RecMateriais = dr["RecMateriais"].ToString();
                        }
                    }
                    cmd.CommandText = "select participantes.IDParticipante, escuteiros.Totem from participantes inner join escuteiros on escuteiros.IDEscuteiro where participantes.IDAtividade = @id";
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) avm.Participantes += dr["Totem"].ToString() + "\r\n";
                    }
                    avm.Participantes = avm.Participantes == null ? "" : avm.Participantes.Substring(0, avm.Participantes.LastIndexOf("\r\n"));
                    cmd.Connection.Close();
                }
                return await Task.Run(() => InserirAtividade((object)avm));
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a edição do registo: " + e.Message;
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        public async Task<IActionResult> Detalhes(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            try
            {
                AtividadeViewModel avm = new AtividadeViewModel();
                using (MySqlCommand cmd = new MySqlCommand("select * from atividades where IDAtividade = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
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
                            while (await dr.ReadAsync())
                            {
                                avm.IDAtividade = int.Parse(dr["IDAtividade"].ToString());
                                avm.Nome = dr["Nome"].ToString();
                                avm.Tipo = dr["Tipo"].ToString();
                                avm.Tema = dr["Tema"].ToString();
                                avm.Seccao = dr["Seccao"].ToString();
                                avm.Local = dr["Local"].ToString();
                                avm.DataInicio = DateTime.Parse(dr["DataInicio"].ToString());
                                avm.DataFim = DateTime.Parse(dr["DataInicio"].ToString());
                                avm.Orcamento = decimal.Parse(dr["Orcamento"].ToString());
                                avm.Ativa = bool.Parse(dr["Ativa"].ToString());
                            }
                        }
                    }
                    cmd.CommandText = "select participantes.IDParticipante, escuteiros.Totem from participantes inner join escuteiros on participantes.IDParticipante = escuteiros.IDEscuteiro where IDAtividade = @id";
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) avm.Participantes += dr["Totem"].ToString() + "\r\n";
                    }
                    avm.Participantes = string.IsNullOrEmpty(avm.Participantes) ? "nenhum" : avm.Participantes.Substring(0, avm.Participantes.LastIndexOf("\r\n"));
                    cmd.CommandText = "select * from recursos where IDAtividade = @id";
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            avm.RecFinanceiros = string.IsNullOrEmpty(dr["RecFinanceiros"].ToString()) ? "nenhum" : dr["RecFinanceiros"].ToString();
                            avm.RecHumanos = string.IsNullOrEmpty(dr["RecHumanos"].ToString()) ? "nenhum" : dr["RecHumanos"].ToString();
                            avm.RecMateriais = string.IsNullOrEmpty(dr["RecMateriais"].ToString()) ? "nenhum" : dr["RecMateriais"].ToString();
                        }
                    }
                    cmd.Connection.Close();
                }
                return await Task.Run(() => View(avm));
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a apresentação dos detalhes do registo: " + e.Message;
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        #region Eliminar
        public async Task<IActionResult> Eliminar(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (!User.IsInRole("Administração de Agrupamento")) return Forbid();
            try
            {
                AtividadeViewModel avm = new AtividadeViewModel();
                using (MySqlCommand cmd = new MySqlCommand("select * from atividades where IDAtividade = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
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
                            while (await dr.ReadAsync())
                            {
                                avm.IDAtividade = int.Parse(dr["IDAtividade"].ToString());
                                avm.Nome = dr["Nome"].ToString();
                                avm.Tipo = dr["Tipo"].ToString();
                                avm.Tema = dr["Tema"].ToString();
                                avm.Seccao = dr["Seccao"].ToString();
                                avm.Local = dr["Local"].ToString();
                                avm.DataInicio = Convert.ToDateTime(dr["DataInicio"].ToString());
                                avm.DataFim = Convert.ToDateTime(dr["DataFim"].ToString());
                                avm.Orcamento = decimal.Parse(dr["Orcamento"].ToString());
                                avm.Ativa = bool.Parse(dr["Ativa"].ToString());
                            }
                        }
                    }
                    cmd.CommandText = cmd.CommandText.Replace("atividades", "recursos");
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            avm.RecFinanceiros = dr["RecFinanceiros"].ToString();
                            avm.RecHumanos = dr["RecHumanos"].ToString();
                            avm.RecMateriais = dr["RecMateriais"].ToString();
                        }
                    }
                    cmd.CommandText = "select count(*) from participantes where IDAtividade = @id";
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) avm.Participantes = dr["count(*)"].ToString() + " participantes";
                    }
                    cmd.Connection.Close();
                }
                return await Task.Run(() => View(avm));
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
            if (!User.IsInRole("Administração de Agrupamento")) return Forbid();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("delete from atividades where IDAtividade = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync();
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.PrepareAsync();
                    int i = await cmd.ExecuteNonQueryAsync();
                    if (i == 0)
                    {
                        cmd.Connection.Close();
                        throw new Exception($"não foi encontrado um registo com o ID \"{id}\"");
                    }
                    cmd.CommandText = cmd.CommandText.Replace("atividades", "participantes");
                    await cmd.PrepareAsync();
                    await cmd.ExecuteNonQueryAsync();
                    cmd.CommandText = cmd.CommandText.Replace("participantes", "recursos");
                    await cmd.PrepareAsync();
                    await cmd.ExecuteNonQueryAsync();
                    cmd.Connection.Close();
                }
            }
            catch (MySqlException mse)
            {
                if (mse.Number == 1451) TempData["msg"] = "esta atividade tem dados associados a si mesmo, como registos de recursos e participantes associados a esta. Procure esses dados e remova as ligações a esta atividade, de modo a eliminá-la com segurança.";
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a eliminação do registo: " + e.Message;
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        #endregion
    }
}