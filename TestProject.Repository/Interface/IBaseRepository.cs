
using System;
using System.Linq;
using System.Linq.Expressions;

namespace TestProject.Repository.Interface
{
    public interface IBaseRepository<T> where T : class
    {

        void Save();

        T Create(T item);

        T Get(object id);

        T Search(Expression<Func<T, bool>> predicate);

        IQueryable<T> Query(Expression<Func<T, bool>> predicate);

    }
}
