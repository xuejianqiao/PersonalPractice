using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class DataBaseConfig
    {
        private static string DefaultSqlConnectionString = @"Database=World;Host=localhost;Port=3306;User Id=root;Password=xuejian123;charset=utf8;pooling=true;";
        public static IDbConnection GetSqlConnection(string sqlConnectionString = null)
        {
            if (string.IsNullOrWhiteSpace(sqlConnectionString))
            {
                sqlConnectionString = DefaultSqlConnectionString;
            }
            IDbConnection conn = new MySqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }
    }
}
