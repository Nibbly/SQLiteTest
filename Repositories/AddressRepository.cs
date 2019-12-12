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
            ExecuteSQLQuery($"INSERT INTO {GetTableName()} (Street, HouseNumber, PostalCode, City, AdditionalInformation) VALUES('{entity.Street}', '{entity.HouseNumber}', '{entity.PostalCode}', '{entity.AddressLocality}', '{entity.AdditionalInformation}')");
        }

        public override Address FindById(int id)
        {
            SQLiteDataReader sqlite_datareader = GetSQLiteDataReader($"SELECT * FROM {GetTableName()} WHERE Id = " + id);
            Address address = new Address();

            while (sqlite_datareader.Read())
            {
                address.Id = sqlite_datareader.GetInt32(0);
                address.Street = sqlite_datareader.GetString(1);
                address.HouseNumber = sqlite_datareader.GetString(2);
                address.PostalCode = sqlite_datareader.GetString(3);
                address.AddressLocality = sqlite_datareader.GetString(4);
                address.AdditionalInformation = sqlite_datareader.GetString(5);
            }

            return address;
        }

        public override List<Address> GetAll()
        {
            SQLiteDataReader sqlite_datareader = GetSQLiteDataReader($"SELECT * FROM {GetTableName()}");
            List<Address> addresses = new List<Address>();
            Address address = new Address();

            while (sqlite_datareader.Read())
            {
                address.Id = sqlite_datareader.GetInt32(0);
                address.Street = sqlite_datareader.GetString(1);
                address.HouseNumber = sqlite_datareader.GetString(2);
                address.PostalCode = sqlite_datareader.GetString(3);
                address.AddressLocality = sqlite_datareader.GetString(4);
                address.AdditionalInformation = sqlite_datareader.GetString(5);

                addresses.Add(address);
                address = new Address();
            }

            return addresses;
        }

        public override void Update(Address entity)
        {
            ExecuteSQLQuery($"UPDATE {GetTableName()} SET Street = '{entity.Street}', HouseNumber = '{entity.HouseNumber}', PostalCode = '{entity.PostalCode}', City = '{entity.AddressLocality}', AdditionalInformation = '{entity.AdditionalInformation}' WHERE Id = {entity.Id}");
        }

        public int GetId(Address address)
        {
            string command = $"SELECT Id FROM {GetTableName()} WHERE Street = '{address.Street}' AND HouseNumber = '{address.HouseNumber}' AND PostalCode = '{address.PostalCode}' AND City = '{address.AddressLocality}'";

            if (!string.IsNullOrEmpty(address.AdditionalInformation))
                command += $"AND AdditionalInformation = '{address.AdditionalInformation}'";

            SQLiteDataReader sqlite_datareader = GetSQLiteDataReader(command);

            int id = 0;

            while (sqlite_datareader.Read())
            {
                id = sqlite_datareader.GetInt32(0);
            }

            return id;
        }

        public AddressRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

    }
}
