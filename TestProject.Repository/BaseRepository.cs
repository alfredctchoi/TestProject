using System;
using System.Linq;
using System.Linq.Expressions;
using TestProject.Database.Context.Interface;
using TestProject.Repository.Interface;

namespace TestProject.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        private readonly IContext _context;

        public BaseRepository(IContext context)
        {
            _context = context;
        }

        public T Create(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
            return item;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public T Get(object id)
        {
            return _context.Set<T>().Find(id);
        }

        public T Search(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }
    }
}
