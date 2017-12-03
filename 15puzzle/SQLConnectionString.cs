using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15puzzle
{
    //REVIEW: Вообще, тут Singleton напрашивается. Или как минимум статика.
    class SQLConnectionString
    {
        public string ConnectionString;
        public SQLConnectionString()
        {
            SqlConnectionStringBuilder connectionstring = new SqlConnectionStringBuilder();
            connectionstring.IntegratedSecurity = true;
            connectionstring.DataSource = Environment.MachineName + "\\SQLEXPRESS";
            connectionstring.InitialCatalog = "15puzzle";
            ConnectionString = connectionstring.ConnectionString;
        }
    }
}
