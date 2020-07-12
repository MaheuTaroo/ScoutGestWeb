using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using ScoutGestWeb.Models;
using MySql.Data.MySqlClient;
using System.Data;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Cryptography.X509Certificates;

namespace ScoutGestWeb.Controllers
{
    [RequireHttps]
    public class EscuteirosController : Controller
    {
        List<string> grupos = new List<string>();
        bool insert = true;
        private readonly UserManager<ApplicationUser> _userManager;
        public EscuteirosController(UserManager<ApplicationUser> userManager) => _userManager = userManager;
        public async Task<IActionResult> Index(string coluna, string procura)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (TempData["msg"] != null) TempData.Keep("msg");
                //Ligar à base de dados e selecionar todos os valores de escuteiros onde IDEscuteiro é maior que 0
                List<EscuteirosViewModel> escuteiros = new List<EscuteirosViewModel>();
                using (MySqlCommand cmd = new MySqlCommand("select * from escuteiros where IDEscuteiro > 0;", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    //Abrir a ligação
                    if (cmd.Connection.State == ConnectionState.Closed) await cmd.Connection.OpenAsync(); ;
                    if (User.IsInRole("Equipa de Animação") || User.IsInRole("Comum"))
                    {
                        cmd.CommandText = cmd.CommandText.Replace(";", " and Seccao = @seccao;");
                        cmd.Parameters.AddWithValue("@seccao", (await _userManager.GetUserAsync(User)).Seccao);
                    }
                    if (!(string.IsNullOrEmpty(coluna) && string.IsNullOrEmpty(procura)))
                    {
                        if (coluna == "Idade" && !int.TryParse(procura, out int idade)) ModelState.AddModelError("", "Idade com formato errado. Por favor, insira um valor numérico");
                        else
                        {
                            cmd.CommandText = cmd.CommandText.Replace(";", " and pesquisa;");
                            switch (coluna)
                            {
                                case "Nome":
                                case "Totem":
                                case "Cargo":
                                case "Localidade":
                                case "Alergias":
                                case "Problemas":
                                case "Estado":
                                    cmd.CommandText = cmd.CommandText.Replace("pesquisa", coluna + " like '%" + procura + "%'");
                                    break;
                                case "Código-postal":
                                    cmd.CommandText = cmd.CommandText.Replace("pesquisa", "CodPostal like '%" + procura + "%'");
                                    break;
                                case "Morada":
                                    cmd.CommandText = cmd.CommandText.Replace("pesquisa", "Morada like '%" + procura + "%' or Morada like '%" + procura + "%'");
                                    break;
                                case "Secção":
                                    cmd.CommandText = cmd.CommandText.Replace("pesquisa", "Seccao like '%" + procura + "%'");
                                    break;
                                case "Grupo sanguíneo":
                                    cmd.CommandText = cmd.CommandText.Replace("pesquisa", "GrupoSang like '%" + procura + "%'");
                                    break;
                                case "Medicação":
                                    cmd.CommandText = cmd.CommandText.Replace("pesquisa", "Medicacao like '%" + procura + "%'");
                                    break;
                                case "Observações":
                                    cmd.CommandText = cmd.CommandText.Replace("pesquisa", "Observacoes like '%" + procura + "%'");
                                    break;
                                case "Idade":
                                    cmd.CommandText = cmd.CommandText.Replace("pesquisa", "Idade = " + int.Parse(procura));
                                    break;
                                case "Número de telefone":
                                    cmd.CommandText = cmd.CommandText.Replace("> 0 and @pesq", "in (select IDEscuteiro from numtelefones where NumTelefone like \'%@tel\'%");
                                    cmd.Parameters.AddWithValue("@tel", procura);
                                    break;
                            }
                        }
                    }
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        //Vai buscar a seguinte informação à base de dados e coloca na tabela
                        int i = 0;
                        while (await dr.ReadAsync())
                        {
                            escuteiros.Add(new EscuteirosViewModel()
                            {
                                ID = int.Parse(dr["IDEscuteiro"].ToString()),
                                Nome = dr["Nome"].ToString(),
                                FotoDown = "data:image/png;base64," + Convert.ToBase64String((byte[])dr["Foto"]),
                                Totem = dr["Totem"].ToString(),
                                Morada = dr["Morada"].ToString(),
                                Morada2 = dr["Morada2"].ToString(),
                                CodPostal = dr["CodPostal"].ToString(),
                                Alergias = dr["Alergias"].ToString(),
                                Medicacao = dr["Medicacao"].ToString(),
                                Problemas = dr["Problemas"].ToString(),
                                Observacoes = dr["Observacoes"].ToString(),
                                Idade = int.Parse(dr["Idade"].ToString()),
                                Seccao = dr["Seccao"].ToString()
                            });
                            i++;
                        }
                    }
                }
                return await Task.Run(() => View(escuteiros));
            }
            return await Task.Run(() => RedirectToAction("Index", "Home"));
        }
        #region Novo escuteiro
        [HttpGet]
        public async Task<IActionResult> InserirEscuteiro()
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            insert = true;
            EscuteirosViewModel evm = new EscuteirosViewModel();
            using (MySqlCommand cmd = new MySqlCommand("select max(IDEscuteiro) from escuteiros", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
            {
                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) evm.ID = Convert.ToInt32(dr["max(IDEscuteiro)"].ToString()) + 1;
                }
                cmd.CommandText = "select IDGrupo, Nome from grupos;";
                using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync()) grupos.Add($"{dr["IDGrupo"]} - {dr["Nome"]}");
                }
            }
            grupos.Sort();
            ViewBag.grupos = grupos;
            return await Task.Run(() => View(evm));
        }
        public async Task<IActionResult> InserirEscuteiro(object model)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            if (TempData["insert"] != null) TempData.Keep("insert");
            return await Task.Run(() => View("InserirEscuteiro", model));
        }
        [HttpPost]
        public async Task<IActionResult> InserirEscuteiro(EscuteirosViewModel insert, int? idold = null)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            try
            {
                if (ModelState.IsValid)
                {
                    List<Cargos> cargos = new List<Cargos>(11), selecionados = new List<Cargos>();
                    cargos.AddRange(new Cargos[11] { insert.Guia, insert.Animador, insert.Cozinheiro, insert.GuardaMaterial, insert.Secretario, insert.Tesoureiro, insert.RelPub, insert.Socorrista, insert.GuiaRegiao, insert.SubGuia, insert.Chefe });
                    foreach (Cargos cargo in cargos)
                    {
                        if (cargo.Selecionado) selecionados.Add(cargo);
                    }
                    if (selecionados.Count > 1 && selecionados.Contains(insert.Guia))
                    {
                        ModelState.AddModelError("Demasiados cargos selecionados", "Foi selecionado o cargo de guia, juntamente com outros cargos. Por favor, selecione o cargo de guia individualmente, ou exclua esse cargo das seleções");
                        return await Task.Run(() => View(insert));
                    }
                    else if (selecionados.Count > 3)
                    {
                        ModelState.AddModelError("Demasiados cargos selecionados", "Foram selecionados cargos em excesso, e o limite máximo é de 3 cargos. Por favor, selecione uma quantidade de cargos dentro do limite");
                        return await Task.Run(() => View(insert));
                    }
                    //Tenta inserir os seguintes valores na tabela escuteiros
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand(this.insert ? "insert into escuteiros values(@id, @nome, @totem, @foto, @grupo, @seccao, @estado, @cargos, @idade, @morada, @morada2, @codpostal, @gruposanguineo, @alergias, @medicacao, @problemas, @observacoes)" : "update escuteiros set IDEscuteiro = @id, Nome = @nome, Totem = @totem, Cargo = @cargos, Grupo = @grupo, Morada = @morada, Morada2 = @morada2, CodPostal = @codpostal, Localidade = @localidade, Alergias = @alergias, Medicacao = @medicacao, Problemas = @problemas, Observacoes = @observacoes, Seccao = @seccao, Estado = @estado, Idade = @idade, Foto = @foto where IDEscuteiro = @idold", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                        {
                            if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                            cmd.Parameters.AddWithValue("@id", insert.ID);
                            cmd.Parameters.AddWithValue("@nome", insert.Nome);
                            cmd.Parameters.AddWithValue("@totem", insert.Totem);
                            cmd.Parameters.AddWithValue("@grupo", insert.Grupo.Substring(0, insert.Grupo.IndexOf(" - ")));
                            if (insert.FotoUp == null) cmd.Parameters.AddWithValue("@foto", '\0');
                            else
                            {
                                using (MemoryStream ms = new MemoryStream())
                                using (Image i = Image.Load(insert.FotoUp.OpenReadStream()))
                                {
                                    i.Save(ms, SixLabors.ImageSharp.Formats.Png.PngFormat.Instance);
                                    cmd.Parameters.AddWithValue("@foto", ms.ToArray());
                                }
                            }
                            cmd.Parameters.AddWithValue("@seccao", insert.Seccao);
                            //Inserir valores na base de dados
                            cmd.Parameters.AddWithValue("@estado", insert.Estado);
                            string cargosDB = "";
                            foreach (Cargos c in selecionados) cargosDB += c.Cargo + ',';
                            cmd.Parameters.AddWithValue("@cargos", cargosDB.Substring(0, cargosDB.LastIndexOf(',')));
                            cmd.Parameters.AddWithValue("@idade", insert.Idade);
                            cmd.Parameters.AddWithValue("@morada", insert.Morada);
                            cmd.Parameters.AddWithValue("@morada2", insert.Morada2);
                            cmd.Parameters.AddWithValue("@codpostal", insert.CodPostal);
                            cmd.Parameters.AddWithValue("@gruposanguineo", insert.GrupoSanguineo);
                            cmd.Parameters.AddWithValue("@alergias", insert.Alergias);
                            cmd.Parameters.AddWithValue("@medicacao", insert.Medicacao);
                            cmd.Parameters.AddWithValue("@problemas", insert.Problemas);
                            cmd.Parameters.AddWithValue("@observacoes", insert.Observacoes);
                            await cmd.PrepareAsync();
                            await cmd.ExecuteNonQueryAsync();
                            cmd.CommandText = this.insert ? "insert into numtelefones values(@id, @telefone)" : "update numtelefones set IDEscuteiro = @id, NumTelefone = @telefone where IDEscuteiro = @idold";
                            cmd.Parameters.AddWithValue("@telefone", "+351" + insert.NumTelefone.Replace(" ", ""));
                            await cmd.PrepareAsync();
                            await cmd.ExecuteNonQueryAsync();
                        }
                        return await Task.Run(() => RedirectToAction("Index"));
                    }
                    catch (MySqlException mse)
                    {
                        //Em caso haja erros na inserção de dados 
                        ModelState.AddModelError("Erro de inserção na base de dados", mse.Message);
                        return await Task.Run(() => View("InserirEscuteiro", insert));
                    }
                }
                using (MySqlCommand cmd = new MySqlCommand("select IDGrupo, Nome from grupos;", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                {
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) grupos.Add($"{dr["IDGrupo"]} - {dr["Nome"]}");
                    }
                }
                grupos.Sort();
                ViewBag.grupos = grupos;
                return await Task.Run(() => View("InserirEscuteiro"));
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a " + (this.insert ? "inserção" : "atualização") + " do registo: " + e.Message;
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        #endregion
        public async Task<IActionResult> Editar(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            EscuteirosViewModel evm = new EscuteirosViewModel();
            try
            {
                List<string> grupos = new List<string>();
                using (MySqlCommand cmd = new MySqlCommand("select escuteiros.*, grupos.Nome as NomeGrupo from escuteiros inner join grupos on escuteiros.Grupo = grupos.IDGrupo where escuteiros.IDEscuteiro = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
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
                                evm.Nome = dr["Nome"].ToString();
                                evm.Totem = dr["Totem"].ToString();
                                evm.Grupo = $"{dr["Grupo"]} - {dr["NomeGrupo"]}";
                                evm.Morada = dr["Morada"].ToString();
                                evm.Morada2 = dr["Morada2"].ToString();
                                evm.CodPostal = dr["CodPostal"].ToString();
                                evm.Localidade = dr["Localidade"].ToString();
                                evm.GrupoSanguineo = dr["GrupoSang"].ToString();
                                evm.Alergias = dr["Alergias"].ToString();
                                evm.Medicacao = dr["Medicacao"].ToString();
                                evm.Problemas = dr["Problemas"].ToString();
                                evm.Observacoes = dr["Observacoes"].ToString();
                                evm.Seccao = dr["Seccao"].ToString();
                                evm.Estado = dr["Estado"].ToString();
                                evm.Idade = int.Parse(dr["Idade"].ToString());
                                evm.FotoDown = "data:image/png;base64," + Convert.ToBase64String((byte[])dr["Foto"]);
                                if (dr["Cargo"].ToString().Contains(evm.Guia.Cargo)) evm.Guia.Selecionado = true;
                                if (dr["Cargo"].ToString().Contains(evm.Animador.Cargo)) evm.Animador.Selecionado = true;
                                if (dr["Cargo"].ToString().Contains(evm.Cozinheiro.Cargo)) evm.Cozinheiro.Selecionado = true;
                                if (dr["Cargo"].ToString().Contains(evm.GuardaMaterial.Cargo)) evm.GuardaMaterial.Selecionado = true;
                                if (dr["Cargo"].ToString().Contains(evm.Secretario.Cargo)) evm.Secretario.Selecionado = true;
                                if (dr["Cargo"].ToString().Contains(evm.Tesoureiro.Cargo)) evm.Tesoureiro.Selecionado = true;
                                if (dr["Cargo"].ToString().Contains(evm.RelPub.Cargo)) evm.RelPub.Selecionado = true;
                                if (dr["Cargo"].ToString().Contains(evm.Socorrista.Cargo)) evm.Socorrista.Selecionado = true;
                                if (dr["Cargo"].ToString().Contains(evm.GuiaRegiao.Cargo)) evm.GuiaRegiao.Selecionado = true;
                                if (dr["Cargo"].ToString().Contains(evm.SubGuia.Cargo)) evm.SubGuia.Selecionado = true;
                                if (dr["Cargo"].ToString().Contains(evm.Chefe.Cargo)) evm.Chefe.Selecionado = true;
                            }
                        }
                    }
                    cmd.CommandText = "select NumTelefone from numtelefones where IDEscuteiro = @id";
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) evm.NumTelefone += dr["NumTelefone"].ToString() + ", ";
                    }
                    evm.NumTelefone = evm.NumTelefone.Substring(0, evm.NumTelefone.LastIndexOf(", "));
                    grupos.Sort();
                    ViewBag.grupos = grupos;
                    TempData["insert"] = insert = false;
                    return await Task.Run(() => RedirectToAction("InserirEscuteiro", (object)evm));
                }
            }
            catch (Exception e)
            {
                TempData["msg"] = "Ocorreu um erro com a edição do registo: " + e.Message;
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        public async Task<IActionResult> Detalhes(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    EscuteirosViewModel evm = null;
                    using (MySqlCommand cmd = new MySqlCommand("select escuteiros.*, grupos.Nome as NomeGrupo from escuteiros inner join grupos on escuteiros.Grupo = grupos.IDGrupo where escuteiros.IDEscuteiro = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
                    {
                        if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@id", id);
                        await cmd.PrepareAsync();
                        using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                        {
                            if (!dr.HasRows) throw new Exception($"não foi encontrado nenhum registo com o ID \"{id}\"");
                            else
                            {
                                while (await dr.ReadAsync()) evm = new EscuteirosViewModel()
                                {
                                    ID = int.Parse(dr["IDEscuteiro"].ToString()),
                                    Nome = dr["Nome"].ToString(),
                                    Totem = dr["Totem"].ToString(),
                                    Morada = dr["Morada"].ToString(),
                                    Morada2 = dr["Morada2"].ToString(),
                                    CodPostal = dr["CodPostal"].ToString(),
                                    GrupoSanguineo = dr["GrupoSang"].ToString(),
                                    Alergias = dr["Alergias"].ToString(),
                                    Medicacao = dr["Medicacao"].ToString(),
                                    Problemas = dr["Problemas"].ToString(),
                                    Observacoes = dr["Observacoes"].ToString(),
                                    Seccao = dr["Seccao"].ToString(),
                                    Idade = int.Parse(dr["Idade"].ToString()),
                                    FotoDown = "data:image/png;base64," + Convert.ToBase64String((byte[])dr["Foto"])
                                };
                            }
                        }
                        cmd.CommandText = "select NumTelefone from numtelefones";
                        using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync()) evm.NumTelefone = dr["NumTelefone"].ToString() + ", ";
                        }
                        evm.NumTelefone = evm.NumTelefone == "" ? "nenhum número de telefone" : evm.NumTelefone.Substring(0, evm.NumTelefone.LastIndexOf(", "));
                        return await Task.Run(() => View(evm));
                    }
                }
                catch (Exception e)
                {
                    TempData["msg"] = "Ocorreu um erro com a apresentação dos detalhes do registo: " + e.Message;
                }
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => RedirectToAction("Index", "Home"));
        }
        #region Eliminar
        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            try
            {
                EscuteirosViewModel evm = new EscuteirosViewModel();
                using (MySqlCommand cmd = new MySqlCommand("select escuteiros.* grupos.Nome as NomeGrupo from escuteiros inner join grupos on escuteiros.Grupo = grupos.IDGrupo where escuteiros.IDEscuteiro = @id" + (!User.IsInRole("Administração de Agrupamento") ? " and Seccao = @seccao;" : ";"), new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
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
                                evm.ID = int.Parse(dr["IDEscuteiro"].ToString());
                                evm.Nome = dr["Nome"].ToString();
                                evm.Grupo = $"{dr["Grupo"]} - {dr["NomeGrupo"]}";
                                evm.Totem = dr["Totem"].ToString();
                                evm.Cargos = dr["Cargo"].ToString().Replace(",", ", ");
                                evm.Morada = dr["Morada"].ToString();
                                evm.Morada2 = dr["Morada2"].ToString();
                                evm.CodPostal = dr["CodPostal"].ToString();
                                evm.Localidade = dr["Localidade"].ToString();
                                evm.GrupoSanguineo = dr["GrupoSang"].ToString();
                                evm.Alergias = dr["Alergias"].ToString();
                                evm.Medicacao = dr["Medicacao"].ToString();
                                evm.Problemas = dr["Problemas"].ToString();
                                evm.Observacoes = dr["Observacoes"].ToString();
                                evm.Seccao = dr["Seccao"].ToString();
                                evm.Estado = dr["Estado"].ToString();
                                evm.FotoDown = "data:image/png;base64," + Convert.ToBase64String((byte[])dr["Foto"]);
                            }
                        }
                    }
                    cmd.CommandText = "select NumTelefone from numtelefones where IDEscuteiro = @id";
                    await cmd.PrepareAsync();
                    using (MySqlDataReader dr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync()) evm.NumTelefone += dr["NumTelefone"].ToString() + ", ";
                    }
                    evm.NumTelefone = evm.NumTelefone == null ? "nenhum número de telefone" : evm.NumTelefone.Substring(0, evm.NumTelefone.LastIndexOf(", "));
                }
                return await Task.Run(() => View("Eliminar", evm));
            }
            catch (Exception e)
            {
                TempData["msg"] = "ocorreu um erro com a eliminação do registo: " + e.Message;
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        [HttpPost]
        public async Task<IActionResult> EliminarPost(int id)
        {
            if (!User.Identity.IsAuthenticated) return await Task.Run(() => RedirectToAction("Index", "Home"));
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("delete from escuteiros where IDEscuteiro = @id", new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root")))
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
    }
}

//Isto pode tar no home controller mas se quiseres deixar isto aqui por mim e na boa
//vou deixar tar isto aqui, se calhar dps sobrecarrega o homecontroller
//ok os botoes tao bons a coisa que e preciso tratar e o botao de voltar a listagem
//eu ainda tou a tratar da view de onde vai originar isso
//ooh okiiiiiiiii
//olha tnh d ir, a tarde volto ok? qrs q te volte a convidar dps?
//tipo eu a tarde vou avançar no relatorio mas i guess eu posso voltar 
//na pap? eu ainda n comecei lmao h e l p
//e melhor começares, e em caso precisares de ajuda nisso eu ajudo na boa
//:ok_hand:
//fuck me, como e q esta merda n consegue receber um valor t simples q aparece na bd
//eu ate diria que talvez seja alguma cena da db mas nao e pois nao?
//uhmmm u there?
/*sry, went afk for some time
//managed to get the error?*/
/*tbh no mas eu tive a limpar uma beca do codigo que copiei e tb alterei so uma cena tua, 
//em vez de meteres 2020 meti a cena do datetime year now*/
/*eu n meti isso, mas i guess it also works xd
mas testaste a linha q eu pus no mysqlcommand na base de dados q te mandei?*/
//como assim? qual linha
/*"select IDSeccao from seccoes where Nome = \"@seccao\"" mas so q em vez do @seccao punhas o q puseste no select
 u there?*/
//s just reading the thing
//smp q eu punha qq cena q tivesse na bd (ex 'Lobitos') returnava qq cena, mas o mysql connector n retorna. some weird shet in here
//lemme try to build to see whats goin on
//oki
/*isto encontra dados qd meto explicitamente o q quero, mas qd isto le do select n encontra nd xd
maybe having aspas on the command doesnt help at all...?*/
//tas a utilizar a mesma bd que me mandaste ainda a ppouco?
//dont write while im debugging, isto tava a ir p onde tava o teu comment. mas sim, obviamente
//a bd nao tem a teabela seccoes
//tem sim, tu e q n encontras. mas deixa exportar a bd again
//tb, entao manda la
//mas tas a ver q bd? a q te mandei alone ou a q tava dentro do zip?
//a que mandaste alone
//essa era a da gestao d atividades, essa e antiga. a q tamos a usar agr ta dentro do zip. se reparares, no ficheiro da bd alone, a data d exportacao ainda data ao mes d marco, qd esta q te mandei dentro do zip data a hj
/*ok, now its good
but what were u sayin?*/
//so tava a dizer que ia substituuir a antiga pela nova
//hey for now im going to leava, a tarde vou continuar a ver os vids e se nao for muito tarde
//eu mando te mensagem
//rn?