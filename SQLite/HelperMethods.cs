using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite
{
    public static class HelperMethods
    {
        public static DBConnection GetConnection()
        {
            return new DBConnection();
        }

        public static SQLiteConnection CreateConnection(string dbName)
        {
            string databaseName = dbName;

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection($"Data Source={databaseName}; Version = 3; New = True; Compress = True; ");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                return new SQLiteConnection();
            }
            return sqlite_conn;
        }
    }
}
