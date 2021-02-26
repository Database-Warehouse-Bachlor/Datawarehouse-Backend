using Npgsql;
using System;

namespace Datawarehouse_Backend.App_Data
{
    class DatabaseProvider
    {
        public void setupDbConnection()
        {
            // Connection string. Used to establish connection to the database
            string connLoginString = "Host=localhost;Port=5432;Username=test;Password=test;Database=logindb";
            string connWarehouse = "Host=localhost;Port=5433;Username=admin;Password=admin;Database=datawarehouse";

            // NpgSqlConnection object is created. This is used to open a connection to the database
            NpgsqlConnection loginConn = new NpgsqlConnection(connLoginString);
            NpgsqlConnection warehouseConn = new NpgsqlConnection(connWarehouse);
            
            // Opens database connection
            loginConn.Open();
            warehouseConn.Open();

            string sqlCommand = "SELECT version()";
            var logincmd = new NpgsqlCommand(sqlCommand, loginConn);
            var warehousecmd = new NpgsqlCommand(sqlCommand, warehouseConn);

            var loginversion = logincmd.ExecuteScalar().ToString();
            var warehouseversion = warehousecmd.ExecuteScalar().ToString();

            Console.WriteLine($"PostgreSQL login version: {loginversion}\nPostgreSQL warehouse version: {warehouseversion}");
        }

    }

}