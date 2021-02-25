using Npgsql;
using System;

namespace Datawarehouse_Backend.App_Data
{
    class DatabaseProvider
    {
        public void dbSetup()
        {
            // Connection string. Used to establish connection to the database
            string connString = "Host=localhost;Port=5432;Username=test;Password=test;Database=logindb";

            // NpgSqlConnection object is created. This is used to open a connection to the database
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            
            // Opens database connection
            conn.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
        }
    }

}