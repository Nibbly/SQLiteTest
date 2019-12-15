using DataAccess;
using Model;
using Repositories;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            SQLiteConnection _context = new SQLiteConnection(ConnectionsStrings.COMPANY_DB_CONNECTIONSTRING);

            _context.Open();

            string query = "select name from pragma_table_info('Company')";
            SQLiteCommand cmd = _context.CreateCommand();
            cmd.CommandText = query;
            SQLiteDataReader sqlite_datareader;
            sqlite_datareader = cmd.ExecuteReader();

            List<string> columns = new List<string>();

            while (sqlite_datareader.Read())
            {
                string col = sqlite_datareader.GetValues()[0];
                columns.Add(col);
            }

            Console.ReadKey();
            
            //UnitOfWork unit = new UnitOfWork();


            //Company c = new Company() { Name = "Aquilas", FirstAddress = GetAddress(), SecondAddress = new Address(), JuristicalNature = JuristicalNature.Company, UseFrequency = UseFrequency.TwoTimesAYear };

            ////unit.CompanyRepository.Add(c);

            //unit.CompanyRepository.Delete(c);



            Console.ReadKey();
        }


        public static SQLiteDataReader GetSQLiteDataReader(string command)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionsStrings.COMPANY_DB_CONNECTIONSTRING))
            {
                conn.Open();
                SQLiteDataReader sqlite_datareader;
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = command;
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                return sqlite_datareader;
            }
        }

        private static Address GetAddress()
        {
            return new Address() { Street = "Neue Straße 2", HouseNumber = "3", PostalCode = "12345", City = "London", AdditionalInformation = "Noch was" };
        }
    }
}
