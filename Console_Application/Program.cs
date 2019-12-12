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
            SQLiteConnection sqlite_connCompanyDB;
            DBConnection connectionCompanyDB = new DBConnection();
            sqlite_connCompanyDB = connectionCompanyDB.CreateConnection(DatabaseNames.COMPANY_DB_NAME);

            SQLiteConnection sqlite_connAddressDB;
            DBConnection connectionAddressDB = new DBConnection();
            sqlite_connAddressDB = connectionAddressDB.CreateConnection(DatabaseNames.ADDRESS_DB_NAME);

            AddressRepository arep = new AddressRepository(sqlite_connAddressDB);

            Company c = GetCompany(sqlite_connCompanyDB, arep);
            CompanyRepository crep = new CompanyRepository(sqlite_connCompanyDB, new AddressRepository(sqlite_connAddressDB));
            crep.Delete(c);


            Console.ReadKey();

            // TEST COMMIT
        }

        private static Address GetAddress()
        {
            return new Address() { Street = "Neue Straße 2", HouseNumber = "3", PostalCode = "12345", AddressLocality = "London", AdditionalInformation = "Noch was" };
        }

        private static void PrintAddress(Address address)
        {
            Console.WriteLine($"Id: {address.Id} Street: {address.Street} Number: {address.HouseNumber} PLZ: {address.PostalCode} City: {address.AddressLocality} Additional: {address.AdditionalInformation}");
        }

        private static Company GetCompany(SQLiteConnection connection, AddressRepository arep)
        {
            CompanyRepository crep = new CompanyRepository(connection, arep);

            Company company = crep.FindById(1);
            
            return company;

        }
    }
}
