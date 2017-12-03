using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15puzzle
{
    static class SQLConnectionString
    {
        static public string MakeSQLConnectionString()
        {
            SqlConnectionStringBuilder connectionstring = new SqlConnectionStringBuilder();
            connectionstring.IntegratedSecurity = true;
            connectionstring.DataSource = Environment.MachineName + "\\SQLEXPRESS";
            connectionstring.InitialCatalog = "15puzzle";
            return connectionstring.ConnectionString;
        }
    }
}
