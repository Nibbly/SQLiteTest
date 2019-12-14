using Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AddressRepository : GenericRepository<Address>, IRepository<Address>
    {
        public override void Add(Address entity)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_context))
            {
                conn.Open();

                ExecuteSQLQuery($"INSERT INTO {GetTableName()} (Street, HouseNumber, PostalCode, City, AdditionalInformation) VALUES('{entity.Street}', '{entity.HouseNumber}', '{entity.PostalCode}', '{entity.City}', '{entity.AdditionalInformation}')");

                conn.Close();
            }
        }

        public override Address FindById(int id)
        {
            Address address = new Address();

            using (SQLiteConnection conn = new SQLiteConnection(_context))
            {
                conn.Open();

                SQLiteDataReader sqlite_datareader = GetSQLiteDataReader($"SELECT * FROM {GetTableName()} WHERE Id = " + id, conn);

                while (sqlite_datareader.Read())
                {
                    address.Id = sqlite_datareader.GetInt32(0);
                    address.Street = sqlite_datareader.GetString(1);
                    address.HouseNumber = sqlite_datareader.GetString(2);
                    address.PostalCode = sqlite_datareader.GetString(3);
                    address.City = sqlite_datareader.GetString(4);
                    address.AdditionalInformation = sqlite_datareader.GetString(5);
                }

                conn.Close();

            }

            return address;
        }

        public override List<Address> GetAll()
        {
            List<Address> addresses = new List<Address>();

            using (SQLiteConnection conn = new SQLiteConnection(_context))
            {
                conn.Open();

                SQLiteDataReader sqlite_datareader = GetSQLiteDataReader($"SELECT * FROM {GetTableName()}", conn);
                Address address = new Address();

                while (sqlite_datareader.Read())
                {
                    address.Id = sqlite_datareader.GetInt32(0);
                    address.Street = sqlite_datareader.GetString(1);
                    address.HouseNumber = sqlite_datareader.GetString(2);
                    address.PostalCode = sqlite_datareader.GetString(3);
                    address.City = sqlite_datareader.GetString(4);
                    address.AdditionalInformation = sqlite_datareader.GetString(5);

                    addresses.Add(address);
                    address = new Address();
                }

                conn.Close();

            }

            return addresses;
        }

        public override void Update(Address entity)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_context))
            {
                conn.Open();

                ExecuteSQLQuery($"UPDATE {GetTableName()} SET Street = '{entity.Street}', HouseNumber = '{entity.HouseNumber}', PostalCode = '{entity.PostalCode}', City = '{entity.City}', AdditionalInformation = '{entity.AdditionalInformation}' WHERE Id = {entity.Id}");

                conn.Close();
            }
        }

        public int GetId(Address address)
        {
            int id = 0;

            using (SQLiteConnection conn = new SQLiteConnection(_context))
            {
                conn.Open();

                string command = $"SELECT Id FROM {GetTableName()} WHERE Street = '{address.Street}' AND HouseNumber = '{address.HouseNumber}' AND PostalCode = '{address.PostalCode}' AND City = '{address.City}'";

                if (!string.IsNullOrEmpty(address.AdditionalInformation))
                    command += $"AND AdditionalInformation = '{address.AdditionalInformation}'";

                SQLiteDataReader sqlite_datareader = GetSQLiteDataReader(command, conn);


                while (sqlite_datareader.Read())
                {
                    id = sqlite_datareader.GetInt32(0);
                }

                conn.Close();

            }

            return id;
        }

        public AddressRepository(string connectionString) : base(connectionString)
        {
            _context = connectionString;
        }
    }
}
