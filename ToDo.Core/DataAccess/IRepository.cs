using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ToDo.Core.DataAccess
{
    public interface IRepository<T>
    {
        T Get(string id);
        T Create(string id, T item);
        T Update(string id, T item);
        void Delete(string id);
        Task<IList<T>> GetList(Expression<Func<T, bool>> filter = null);
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task<int> Count(Expression<Func<T, bool>> filter = null);
        Task<bool> Any(Expression<Func<T, bool>> filter);
    }
}
