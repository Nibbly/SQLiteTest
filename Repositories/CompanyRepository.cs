using Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CompanyRepository : GenericRepository<Company>, IRepository<Company>
    {
        private AddressRepository _addressRepository;

        public override void Add(Company entity)
        {
            _addressRepository.Add(entity.Address);
            int addressId = _addressRepository.GetId(entity.Address);

            ExecuteSQLQuery($"INSERT INTO {GetTableName()} (Name, Address, JuristicalNature, UseFrequency) VALUES('{entity.Name}', '{addressId}', '{(int)entity.JuristicalNature}', '{(int)entity.UseFrequency}')");
        }

        public override Company FindById(int id)
        {
            SQLiteDataReader sqlite_datareader = GetSQLiteDataReader($"SELECT * FROM {GetTableName()} WHERE Id = " + id);
            Company company = new Company();
            Address address = new Address();

            while (sqlite_datareader.Read())
            {
                company.Id = sqlite_datareader.GetInt32(0);
                company.Name = sqlite_datareader.GetString(1);
                company.Address = _addressRepository.FindById(sqlite_datareader.GetInt32(2));
                company.JuristicalNature = (JuristicalNature)sqlite_datareader.GetInt32(3);
                company.UseFrequency = (UseFrequency)sqlite_datareader.GetInt32(4);
            }

            return company;
        }

        public override List<Company> GetAll()
        {
            SQLiteDataReader sqlite_datareader = GetSQLiteDataReader($"SELECT * FROM {GetTableName()}");
            Company company = new Company();
            Address address = new Address();
            List<Company> cList = new List<Company>();

            while (sqlite_datareader.Read())
            {
                company.Id = sqlite_datareader.GetInt32(0);
                company.Name = sqlite_datareader.GetString(1);
                company.Address = _addressRepository.FindById(sqlite_datareader.GetInt32(2));
                company.JuristicalNature = (JuristicalNature)sqlite_datareader.GetInt32(3);
                company.UseFrequency = (UseFrequency)sqlite_datareader.GetInt32(4);

                cList.Add(company);
                company = new Company();
                address = new Address();
            }

            return cList;
        }

        public override void Update(Company entity)
        {
            ExecuteSQLQuery($"UPDATE {GetTableName()} SET Name = '{entity.Name}', Address = '{_addressRepository.GetId(entity.Address)}', JuristicalNature = '{(int)entity.JuristicalNature}', UseFrequency = '{(int)entity.UseFrequency}' WHERE Id = {entity.Id}");
        }

        public int GetId(Company company)
        {
            string command = $"SELECT Id FROM {GetTableName()} WHERE Name = '{company.Name}' AND Address = '{_addressRepository.GetId(company.Address)}' AND JuristicalNature = '{company.JuristicalNature}' AND UseFrequency = '{company.UseFrequency}'";

            SQLiteDataReader sqlite_datareader = GetSQLiteDataReader(command);

            int id = 0;

            while (sqlite_datareader.Read())
            {
                id = sqlite_datareader.GetInt32(0);
            }

            return id;
        }

        public CompanyRepository(SQLiteConnection connection, AddressRepository addressRepository)
        {
            _connection = connection;
            _addressRepository = addressRepository;
        }
    }
}

