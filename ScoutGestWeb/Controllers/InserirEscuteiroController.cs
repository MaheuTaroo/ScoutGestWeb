using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using ScoutGestWeb.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace ScoutGestWeb.Controllers
{
    public class InserirEscuteiroController : Controller
    {
        public IActionResult Index()
        {
            //Ligar à base de dados e selecionar todos os valores de escuteiros onde IDEscuteiro é maior que 0
            List<InserirEscuteiroViewModel> escuteiros = new List<InserirEscuteiroViewModel>();
            using (MySqlCommand cmd = new MySqlCommand("select * from escuteiros where IDEscuteiro > 0;", UserData.UserData.con))
            {
                //Abrir a ligação
                if (UserData.UserData.con.State == ConnectionState.Closed) UserData.UserData.con.Open();
                cmd.Prepare();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {  //Vai buscar a seguinte informação à base de dados e coloca na tabela
                    while (dr.Read()) escuteiros.Add(new InserirEscuteiroViewModel()
                    {
                        ID = int.Parse(dr["IDEscuteiro"].ToString()),
                        Nome = dr["Nome"].ToString(),
                        Totem = dr["Totem"].ToString(),
                        Morada = dr["Morada"].ToString(),
                        Morada2 = dr["Morada2"].ToString(),
                        CodPostal = dr["CodPostal"].ToString(),
                        Alergias = dr["Alergias"].ToString(),
                        Medicacao = dr["Medicacao"].ToString(),
                        Problemas = dr["Problemas"].ToString(),
                        Observacoes = dr["Observacoes"].ToString(),
                        Idade = int.Parse(dr["Idade"].ToString())
                    });
                }
                cmd.CommandText = "select Cargo from ";
            }
            return View(escuteiros);
        }
        //[Route("/InserirEscuteiro/InserirEscuteiro", Name = "InserirEscuteiro")]
        public IActionResult InserirEscuteiro()
        {
            //login problem not yet solved; trying to adapt to identity
            return UserData.UserData.userData.Count == 0 ? RedirectToAction("Index", "Home") : (IActionResult)View();
        }
        [HttpPost]
        public async Task<IActionResult> InserirEscuteiro(InserirEscuteiroViewModel insert)
        {
            foreach (Cargos cargo in insert._cargo)
            {
                if (cargo.Selecionado == true) insert.Cargo.Add(cargo);
            }
            //Tenta inserir os seguintes valores na tabela escuteiros
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into escuteiros(Nome, Totem, Foto, Seccao, Estado, Cargo, Idade, NumTelefone, Morada, Morada2, CodPostal, GrupoSanguineo, Alergias, Medicacao, Problemas, Observacoes) values(@nome, @totem, @foto, @seccao, @estado, @cargos, @idade, @telefone, @morada, @morada2, @codpostal, @gruposanguineo, @alergias, @medicacao, @problemas, @observacoes)", UserData.UserData.con))
                {
                    cmd.Parameters.AddWithValue("@nome", insert.Nome);
                    cmd.Parameters.AddWithValue("@totem", insert.Totem);
                    if (insert.Foto == null) cmd.Parameters.AddWithValue("@foto", '\0');
                    else
                    {
                        using MemoryStream ms = new MemoryStream();
                        byte[] foto = new byte[0];
                        insert.Foto.OpenReadStream().Read(foto, 0, (int)insert.Foto.OpenReadStream().Length);
                        ms.Write(foto);
                        cmd.Parameters.AddWithValue("@foto", ms.ToArray());
                    }
                    using (MySqlCommand cmd3 = new MySqlCommand("select IDSeccao from seccoes where Nome = @seccao", cmd.Connection))
                    {
                        cmd3.Parameters.AddWithValue("@seccao", insert.Seccao);
                        cmd3.Prepare();
                        using (MySqlDataReader dr3 = cmd3.ExecuteReader()) while (dr3.Read()) cmd.Parameters.AddWithValue("@seccao", dr3["IDSeccao"]);
                        cmd3.CommandText = "select IDEstado from estados where Descricao = @estado";
                        cmd3.Parameters.AddWithValue("@estado", insert.Estado);
                        using (MySqlDataReader dr4 = cmd3.ExecuteReader()) while (dr4.Read()) cmd.Parameters.AddWithValue("@estado", dr4["IDEstado"]);
                    }
                    //Inserir valores na base de dados
                    cmd.Parameters.AddWithValue("@cargos", insert.Cargo);
                    cmd.Parameters.AddWithValue("@idade", insert.Idade);
                    cmd.Parameters.AddWithValue("@telefone", "+351" + insert.NumTelefone);
                    cmd.Parameters.AddWithValue("@morada", insert.Morada);
                    cmd.Parameters.AddWithValue("@morada2", insert.Morada2);
                    cmd.Parameters.AddWithValue("@codpostal", insert.CodPostal);
                    cmd.Parameters.AddWithValue("@gruposanguineo", insert.GrupoSanguineo);
                    cmd.Parameters.AddWithValue("@alergias", insert.Alergias);
                    cmd.Parameters.AddWithValue("@medicacao", insert.Medicacao);
                    cmd.Parameters.AddWithValue("@problemas", insert.Problemas);
                    cmd.Parameters.AddWithValue("@observacoes", insert.Observacoes);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                return await Task.Run(() => View("../../Controllers/HomeController/Index"));
            }
            catch (MySqlException mse)
            {
                //Em caso haja erros na inserção de dados 
                ModelState.AddModelError("Erro de inserção na base de dados", mse.Message);
                Console.WriteLine(ModelState.ErrorCount);
                return View("InserirEscuteiro");
            }
        }
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
//eu ate diria que talvez seja gluma cena da db mas nao e pois nao?
//uhmmm u there?
/*sry, went afk for some time
managed to get the error?*/
/*tbh no mas eu tive a limpar uma beca do codigo que copiei e tb alterei so uma cena tua, 
//em vez de meteres 2020 meti a cena do datetime year now*/
/*eu n meti isso, mas i guess it alo works xd
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