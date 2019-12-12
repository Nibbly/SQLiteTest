using Model;
using Repositories;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RepositoryTests
{

    public class CompanyRepository_Test
    {
        [Fact]
        public void ShouldAddCompanyThatIsNotYetInDatabase()
        {
            SQLiteConnection sqlite_connCompanyDB;
            DBConnection connectionCompanyDB = new DBConnection();
            sqlite_connCompanyDB = connectionCompanyDB.CreateConnection(DatabaseNames.COMPANY_DB_NAME);

            SQLiteConnection sqlite_connAddressDB;
            DBConnection connectionAddressDB = new DBConnection();
            sqlite_connAddressDB = connectionAddressDB.CreateConnection(DatabaseNames.ADDRESS_DB_NAME);

            AddressRepository aRep = new AddressRepository(sqlite_connAddressDB);
            CompanyRepository crep = new CompanyRepository(sqlite_connCompanyDB, aRep);

            Company company = GetCompany();
            crep.Add(company);

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_connCompanyDB.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT Id FROM Company WHERE Name = '{company.Name}' AND Address = '{aRep.GetId(company.Address)}' AND JuristicalNature = '{(int)company.JuristicalNature}' AND UseFrequency = '{(int)company.UseFrequency}'";
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                company.Id = sqlite_datareader.GetInt32(0);
            }

            int numOfRows = crep.GetAll().Count;
            crep.Delete(company);
            int numOfRowsNew = crep.GetAll().Count;

            Assert.Equal(numOfRows - 1, numOfRowsNew);
        }

        private static Address GetAddress()
        {
            return new Address() { Street = "Neue Straße 2", HouseNumber = "3", PostalCode = "12345", AddressLocality = "London", AdditionalInformation = "Noch was" };
        }

        private static Company GetCompany()
        {
            return new Company() { Name = "Dummy Comp", Address = GetAddress(), JuristicalNature = JuristicalNature.Company, UseFrequency = UseFrequency.EveryMonth };
        }
    }
}
