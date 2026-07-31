using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bulky2_DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T Get(Expression<Func<T, bool>> filter);

        void Add(T entity);
        void Remove(T entity);
        void Removerange(IEnumerable<T> entity);
    }
}
