using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppointmentSystem
{
    internal class SqlConnect
    {
        public SqlConnection sqlConn () 
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GA77R8Q;Initial Catalog=Hospital_Database;Integrated Security=True");
            conn.Open ();
            return conn;
        }
    }
}
