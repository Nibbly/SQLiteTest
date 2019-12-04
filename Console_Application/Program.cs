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
            sqlite_conn = DBConnection.CreateConnectionMyDb();
            //DBTableCreator.CreateTable(sqlite_conn);
            //CRUDOperation.InsertDataMyDb(sqlite_conn, "Ronny", "Singer");
            //CRUDOperation.InsertDataMyDb(sqlite_conn, "Hansi", "Hans");
            CRUDOperation.ReadDataMyDb(sqlite_conn);

            Console.ReadKey();

            // TEST COMMIT
        }
    }
}
