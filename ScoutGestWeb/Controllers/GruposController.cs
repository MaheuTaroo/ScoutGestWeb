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
    public class GruposController : Controller
    {
        public IActionResult Index()
        {
            List<int> seccoes = new List<int>();
            if (UserData.UserData.userData.Count == 0) return RedirectToAction("Index", "Home");
            List<GrupoViewModel> gvm = new List<GrupoViewModel>();
            using (MySqlCommand cmd = new MySqlCommand("select * from grupos", UserData.UserData.con))
            {
                if (UserData.UserData.con.State == ConnectionState.Closed) UserData.UserData.con.Open();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        gvm.Add(new GrupoViewModel()
                        {
                            ID = int.Parse(dr["IDGrupo"].ToString()),
                            Nome = dr["Nome"].ToString(),
                            Sigla = dr["Sigla"].ToString(),
                            Pseudonimo = dr["Pseudonimo"].ToString()
                        });
                        seccoes.Add(int.Parse(dr["Seccao"].ToString()));
                    }
                }
                cmd.CommandText = "select Nome from seccoes where IDSeccao = @id";
                for (int i = 0; i < seccoes.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@id", seccoes[i]);
                    cmd.Prepare();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read()) gvm[i].Seccao = dr["Nome"].ToString();
                    }
                    cmd.Parameters.Clear();
                }
            }
            return View(gvm);
        }
    }
}