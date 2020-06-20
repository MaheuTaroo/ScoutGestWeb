using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ScoutGestWeb.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Não foi introduzido o username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Não foi introduzida a password")]
        public string Password { get; set; }
        /*public bool Login()
        {
            bool success = false;
            StringBuilder sb = new StringBuilder();
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                md5.ComputeHash(Encoding.ASCII.GetBytes(Password));
                for (int i = 0; i < md5.Hash.Length; i++) sb.Append(md5.Hash[i].ToString("x2"));
            }
            using (MySqlCommand cmd = new MySqlCommand("select * from users where User = @user and Pass = @pass", UserData.UserData.con))
            {
                if (cmd.Connection.State == ConnectionState.Closed) if (UserData.UserData.con.State == ConnectionState.Closed) UserData.UserData.con.Open();
                cmd.Parameters.AddWithValue("@user", Username);
                cmd.Parameters.AddWithValue("@pass", sb.ToString());
                cmd.Prepare();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read()) for (int i = 0; i < dr.FieldCount; i++) UserData.UserData.userData.Add(dr.GetSchemaTable().Rows[i].Field<string>("ColumnName"), dr[i].ToString());
                    }
                }
                if (UserData.UserData.userData.Count > 0)
                {
                    cmd.CommandText = "select Nome from grupos where IDGrupo = @id;";
                    cmd.Parameters.AddWithValue("@id", UserData.UserData.userData["IDGrupo"]);
                    cmd.Prepare();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        //i am still puzzled
                        while (dr.Read()) UserData.UserData.userData.Add("Nome", dr["Nome"]);
                    }
                    cmd.CommandText = "select * from atividades inner join seccoes, grupos where seccoes.IDSeccao = atividades.Seccao and grupos.IDGrupo = atividades.Grupo and IDAtividade > 0;";
                    cmd.Prepare();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        success = dr.HasRows;
                    }
                }
            }
            return success;
        }*/
    }
}
