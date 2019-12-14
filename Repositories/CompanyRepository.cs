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
        public override void Add(Company entity)
        {
            try
            {
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = _context.CreateCommand();
                sqlite_cmd.CommandText = $"INSERT INTO {GetTableName()} (Name, Street_1, HouseNumber_1, PostalCode_1, City_1, AdditionalInformation_1, Street_2, HouseNumber_2, PostalCode_2, City_2, AdditionalInformation_2, JuristicalNature, UseFrequency) VALUES('{entity.Name}', '{entity.FirstAddress.Street}', '{entity.FirstAddress.HouseNumber}', '{entity.FirstAddress.PostalCode}', '{entity.FirstAddress.City}', '{entity.FirstAddress.AdditionalInformation}', '{entity.SecondAddress.Street}', '{entity.SecondAddress.HouseNumber}', '{entity.SecondAddress.PostalCode}', '{entity.SecondAddress.City}', '{entity.SecondAddress.AdditionalInformation}', '{(int)entity.JuristicalNature}', '{(int)entity.UseFrequency}')";
                sqlite_cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                //log item probably already in db
            }
        }

        public override Company FindById(int id)
        {
            Company company = new Company() { FirstAddress = new Address(), SecondAddress = new Address() };

            SQLiteDataReader sqlite_datareader = GetSQLiteDataReader($"SELECT * FROM {GetTableName()} WHERE Id = " + id);

            while (sqlite_datareader.Read())
            {
                company.Id = sqlite_datareader.GetInt32(0);
                company.Name = sqlite_datareader.GetString(1);
                company.FirstAddress.Street = sqlite_datareader.GetString(2);
                company.FirstAddress.HouseNumber = sqlite_datareader.GetString(3);
                company.FirstAddress.PostalCode = sqlite_datareader.GetString(4);
                company.FirstAddress.City = sqlite_datareader.GetString(5);
                company.FirstAddress.AdditionalInformation = sqlite_datareader.GetString(6);
                company.SecondAddress.Street = sqlite_datareader.GetString(7);
                company.SecondAddress.HouseNumber = sqlite_datareader.GetString(8);
                company.SecondAddress.PostalCode = sqlite_datareader.GetString(9);
                company.SecondAddress.City = sqlite_datareader.GetString(10);
                company.SecondAddress.AdditionalInformation = sqlite_datareader.GetString(11);
                company.JuristicalNature = (JuristicalNature)sqlite_datareader.GetInt32(12);
                company.UseFrequency = (UseFrequency)sqlite_datareader.GetInt32(13);
            }

            return company;
        }

        public override List<Company> GetAll()
        {
            List<Company> cList = new List<Company>();

            Company company = new Company() { FirstAddress = new Address(), SecondAddress = new Address() };

            SQLiteDataReader sqlite_datareader = GetSQLiteDataReader($"SELECT * FROM {GetTableName()}");

            while (sqlite_datareader.Read())
            {
                company.Id = sqlite_datareader.GetInt32(0);
                company.Name = sqlite_datareader.GetString(1);
                company.FirstAddress.Street = sqlite_datareader.GetString(2);
                company.FirstAddress.HouseNumber = sqlite_datareader.GetString(3);
                company.FirstAddress.PostalCode = sqlite_datareader.GetString(4);
                company.FirstAddress.City = sqlite_datareader.GetString(5);
                company.FirstAddress.AdditionalInformation = sqlite_datareader.GetString(6);
                company.SecondAddress.Street = sqlite_datareader.GetString(7);
                company.SecondAddress.HouseNumber = sqlite_datareader.GetString(8);
                company.SecondAddress.PostalCode = sqlite_datareader.GetString(9);
                company.SecondAddress.City = sqlite_datareader.GetString(10);
                company.SecondAddress.AdditionalInformation = sqlite_datareader.GetString(11);
                company.JuristicalNature = (JuristicalNature)sqlite_datareader.GetInt32(12);
                company.UseFrequency = (UseFrequency)sqlite_datareader.GetInt32(13);

                cList.Add(company);
                company = new Company() { FirstAddress = new Address(), SecondAddress = new Address() };
            }

            return cList;
        }

        public override void Update(Company entity)
        {
            ExecuteSQLQuery($"UPDATE {GetTableName()} SET Name = '{entity.Name}', Street_1 = '{entity.FirstAddress.Street}', HouseNumber_1 = '{entity.FirstAddress.HouseNumber}', PostalCode_1 = '{entity.FirstAddress.PostalCode}', City_1 = '{entity.FirstAddress.City}', AdditionalInformation_1 = '{entity.FirstAddress.AdditionalInformation}', Street_2 = '{entity.SecondAddress.Street}', HouseNumber_2 = '{entity.SecondAddress.HouseNumber}', PostalCode_2 = '{entity.SecondAddress.PostalCode}', City_2 = '{entity.SecondAddress.City}', AdditionalInformation_2 = '{entity.SecondAddress.AdditionalInformation}', JuristicalNature = '{(int)entity.JuristicalNature}', UseFrequency = '{(int)entity.UseFrequency}' WHERE Id = {entity.Id}");
        }

        public int GetId(Company entity)
        {
            int id = 0;

            string command = $"SELECT Id FROM {GetTableName()} WHERE Name = '{entity.Name}' AND Street_1 = '{entity.FirstAddress.Street}' AND HouseNumber_1 = '{entity.FirstAddress.HouseNumber}' AND PostalCode_1 = '{entity.FirstAddress.PostalCode}' AND City_1 = '{entity.FirstAddress.City}' AND AdditionalInformation_1 = '{entity.FirstAddress.AdditionalInformation}' AND Street_2 = '{entity.SecondAddress.Street}' AND HouseNumber_2 = '{entity.SecondAddress.HouseNumber}' AND PostalCode_2 = '{entity.SecondAddress.PostalCode}' AND City_2 = '{entity.SecondAddress.City}' AND AdditionalInformation_2 = '{entity.SecondAddress.AdditionalInformation}' AND JuristicalNature = '{(int)entity.JuristicalNature}' AND UseFrequency = '{(int)entity.UseFrequency}'";

            SQLiteDataReader sqlite_datareader = GetSQLiteDataReader(command);

            while (sqlite_datareader.Read())
            {
                id = sqlite_datareader.GetInt32(0);
            }

            return id;
        }

        public CompanyRepository(SQLiteConnection context)
        {
            _context = context;
        }
    }
}

