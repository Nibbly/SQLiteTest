using Repositories;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private SQLiteConnection _companyConnection = new SQLiteConnection(ConnectionsStrings.COMPANY_DB_CONNECTIONSTRING);
        private CompanyRepository _companyRepository;

        public CompanyRepository CompanyRepository
        {
            get
            {
                if (_companyConnection != null && _companyConnection.State != System.Data.ConnectionState.Open)
                    _companyConnection.Open();

                if (this._companyRepository == null)
                    this._companyRepository = new CompanyRepository(_companyConnection);

                return _companyRepository;
            }
        }

        // Disposing

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _companyConnection.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
