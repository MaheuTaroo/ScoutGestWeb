using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutGestWeb.UserData
{
    public class DateTimeRangeAttribute : ValidationAttribute
    {
        public DateTime Minimum { get; set; }
        public DateTime Maximum = DateTime.Now;
        /// <summary>
        /// Compares a DateTime variable between the minimum specified and the DateTime.Now variable
        /// </summary>
        /// <param name="min">The minimum border to compare the DateTime to</param>
        public DateTimeRangeAttribute(object min)
        {
            Minimum = Convert.ToDateTime(min);
        }
        public override bool IsValid(object value)
        {
            return (DateTime)value >= Minimum && (DateTime)value <= Maximum;
        }
    }
    public static class UserData
    {
        public static readonly MySqlConnection con = new MySqlConnection("server=localhost; port=3306; database=scoutgest; user=root");
        public static Hashtable userData = new Hashtable();
    }
}
