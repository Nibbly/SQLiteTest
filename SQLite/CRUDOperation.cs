using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite
{
    public static class CRUDOperation
    {
        public static void InsertDataMyDb(SQLiteConnection conn, string firstname, string lastname)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = $"INSERT INTO Person (FirstName, LastName) VALUES('{firstname}', '{lastname}'); ";
            sqlite_cmd.ExecuteNonQuery();
        }


        public static void ReadDataMyDb(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Person";

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                int id = sqlite_datareader.GetInt32(0);
                string firstName = sqlite_datareader.GetString(1);
                string lastName = sqlite_datareader.GetString(2);

                Console.WriteLine($"Person: {id} - {firstName} {lastName}");
            }

            conn.Close();
        }
    }
}
