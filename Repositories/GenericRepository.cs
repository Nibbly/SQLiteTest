using Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : IEntity
    {
        protected SQLiteConnection _connection;

        public abstract void Add(T entity);

        public void Delete(T entity)
        {
            ExecuteSQLQuery($"DELETE FROM {GetTableName()} WHERE Id = {entity.Id}");
        }

        public abstract T FindById(int id);

        public abstract List<T> GetAll();

        public abstract void Update(T entity);

        protected void ExecuteSQLQuery(string query)
        {
            try
            {
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = _connection.CreateCommand();
                sqlite_cmd.CommandText = query;
                sqlite_cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //TODO logging
            }
        }

        protected string GetTableName()
        {
            string query = "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY 1";
            SQLiteCommand cmd = _connection.CreateCommand();
            cmd.CommandText = query;
            SQLiteDataReader sqlite_datareader;
            sqlite_datareader = cmd.ExecuteReader();

            return sqlite_datareader.GetValues()[0];
        }

        protected SQLiteDataReader GetSQLiteDataReader(string command)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = _connection.CreateCommand();
            sqlite_cmd.CommandText = command;
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            return sqlite_datareader;
        }
    }
}
