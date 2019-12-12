using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T FindById(int id);
        List<T> GetAll();
    }
}
