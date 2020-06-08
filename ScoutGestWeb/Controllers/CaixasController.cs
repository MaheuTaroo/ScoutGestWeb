using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ScoutGestWeb.Models;

namespace ScoutGestWeb.Controllers
{
    public class CaixasController : Controller
    {
        readonly List<CaixaViewModel> cvm = new List<CaixaViewModel>();
        readonly List<int> grupos = new List<int>(), responsaveis = new List<int>();
        public IActionResult Index()
        {
            if (UserData.UserData.userData.Count == 0) return RedirectToAction("Index", "Home");
            using (MySqlCommand cmd = new MySqlCommand("select * from caixas", UserData.UserData.con))
            {
                if (UserData.UserData.con.State == ConnectionState.Closed) UserData.UserData.con.Open();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cvm.Add(new CaixaViewModel()
                        {
                            ID = int.Parse(dr["IDCaixa"].ToString()),
                            Nome = dr["Nome"].ToString(),
                            Responsavel = dr["Responsavel"].ToString() + " - "
                        });
                        grupos.Add(int.Parse(dr["Grupo"].ToString()));
                        responsaveis.Add(int.Parse(dr["Responsavel"].ToString()));
                    }
                }
                cmd.CommandText = "select Nome from grupos where IDGrupo = @id";
                for (int i = 0; i < grupos.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@id", grupos[i]);
                    cmd.Prepare();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read()) cvm[i].Grupo = dr["Nome"].ToString();
                    }
                    cmd.Parameters.Clear();
                }
                cmd.CommandText = "select Nome from escuteiros where IDEscuteiro = @id";
                for (int i = 0; i < responsaveis.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@id", grupos[i]);
                    cmd.Prepare();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read()) cvm[i].Responsavel += dr["Nome"].ToString();
                    }
                    cmd.Parameters.Clear();
                }
            }
            return View(cvm);
        }
    }
}