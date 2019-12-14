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
        private DBConnection _dbConnectionCompany;
        private DBConnection _dbConnectionAddress;
        private SQLiteConnection _sqliteConnectionCompany;
        private SQLiteConnection _sqliteConnectionAddress;
        private SQLiteDataReader _sqliteDatareader;
        private SQLiteCommand sqlite_cmd;

        public CompanyRepository_Test()
        {
            _dbConnectionCompany = HelperMethods.GetConnection();
            _dbConnectionAddress = HelperMethods.GetConnection();
        }

        [Fact]
        public void ShouldAddCompanyThatIsNotYetInDatabase()
        {
            using (_sqliteConnectionCompany = HelperMethods.CreateConnection(DatabaseNames.COMPANY_DB_NAME))
            {
                using (_sqliteConnectionAddress = HelperMethods.CreateConnection(DatabaseNames.ADDRESS_DB_NAME))
                {
                    AddressRepository aRep = new AddressRepository(_sqliteConnectionAddress);
                    CompanyRepository crep = new CompanyRepository(_sqliteConnectionCompany, aRep);

                    Company company = GetCompany();
                    crep.Add(company);

                    sqlite_cmd = _sqliteConnectionCompany.CreateCommand();
                    sqlite_cmd.CommandText = $"SELECT Id FROM Company WHERE Name = '{company.Name}' AND Address = '{aRep.GetId(company.Address)}' AND JuristicalNature = '{(int)company.JuristicalNature}' AND UseFrequency = '{(int)company.UseFrequency}'";
                    _sqliteDatareader = sqlite_cmd.ExecuteReader();

                    while (_sqliteDatareader.Read())
                    {
                        company.Id = _sqliteDatareader.GetInt32(0);
                    }

                    int numOfRows = crep.GetAll().Count;
                    crep.Delete(company);
                    int numOfRowsNew = crep.GetAll().Count;

                    Assert.Equal(numOfRows - 1, numOfRowsNew);
                }
            }
        }

        [Fact]
        public void ShouldNotAddCompanyThatIsAlreadyInDatabase()
        {
            using (_sqliteConnectionCompany = HelperMethods.CreateConnection(DatabaseNames.COMPANY_DB_NAME))
            {
                using (_sqliteConnectionAddress = HelperMethods.CreateConnection(DatabaseNames.ADDRESS_DB_NAME))
                {
                    Company comp1 = GetCompany();
                    CompanyRepository cRep = new CompanyRepository(_sqliteConnectionCompany, new AddressRepository(_sqliteConnectionAddress));

                    cRep.Add(comp1);

                    int numOfRows = cRep.GetAll().Count;
                    cRep.Add(comp1);
                    int newNumOfRows = cRep.GetAll().Count;

                    Assert.Equal(numOfRows, newNumOfRows);
                }
            }
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
