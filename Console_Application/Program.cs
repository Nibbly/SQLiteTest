using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Application
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = DBConnection.CreateConnection();
            DBTableCreator.CreateTable(sqlite_conn);
            CRUDOperation.InsertData(sqlite_conn);
            CRUDOperation.ReadData(sqlite_conn);

            Console.ReadKey();
        }
    }
}
