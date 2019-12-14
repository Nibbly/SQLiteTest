using DataAccess;
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
            // test setup:
            // empty database for companies - add 1 item
            // check if count of all items increased by 1
            // delete item, so test is always working on a clean database

            UnitOfWork unit = new UnitOfWork();
            unit.CompanyRepository.Add(GetCompany());
            int numOfItemsAfterAdding = unit.CompanyRepository.GetAll().Count;
            unit.CompanyRepository.Delete(GetCompany());

            Assert.Equal(1, numOfItemsAfterAdding);
        }

        [Fact]
        public void ShouldNotAddCompanyThatIsAlreadyInDatabase()
        {
            UnitOfWork unit = new UnitOfWork();
            unit.CompanyRepository.Add(GetCompany());
            unit.CompanyRepository.Add(GetCompany());
            int numOfItemsAfterAdding = unit.CompanyRepository.GetAll().Count;
            unit.CompanyRepository.Delete(GetCompany());

            Assert.Equal(1, numOfItemsAfterAdding);
        }

        [Fact]
        public void ShouldDeleteCompany()
        {
            UnitOfWork unit = new UnitOfWork();
            unit.CompanyRepository.Add(GetCompany());
            int numOfItemsAfterAdding = unit.CompanyRepository.GetAll().Count;

            Assert.Equal(1, numOfItemsAfterAdding);

            Company comp = GetCompany();
            comp.Id = unit.CompanyRepository.GetId(comp);

            unit.CompanyRepository.Delete(comp);
            int numOfItemsAfterDeleting = unit.CompanyRepository.GetAll().Count;

            Assert.Equal(0, numOfItemsAfterDeleting);
        }

        [Fact]
        public void ShouldUpdateCompany()
        {
            UnitOfWork unit = new UnitOfWork();
            unit.CompanyRepository.Add(GetCompany());

            Company comp = unit.CompanyRepository.GetAll().First();
            string newCompName = "Changed Company Name";
            comp.Name = newCompName;

            unit.CompanyRepository.Update(comp);
            Company newComp = unit.CompanyRepository.FindById(comp.Id);

            Assert.Equal(newCompName, newComp.Name);

            unit.CompanyRepository.Delete(newComp);
        }


        private static Address GetAddress()
        {
            return new Address() { Street = "Neue Straße 2", HouseNumber = "3", PostalCode = "12345", City = "London", AdditionalInformation = "Noch was" };
        }

        private static Company GetCompany()
        {
            return new Company() { Name = "Dummy Comp", FirstAddress = GetAddress(), SecondAddress = new Address(), JuristicalNature = JuristicalNature.Company, UseFrequency = UseFrequency.EveryMonth };
        }
    }
}
