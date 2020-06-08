using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutGestWeb.UserData
{
    public static class UserData
    {
        public static readonly MySqlConnection con = new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root");
        public static Hashtable userData = new Hashtable();
    }
}
