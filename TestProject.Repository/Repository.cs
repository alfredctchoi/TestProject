using System;
using TestProject.Database.Context;
using TestProject.Repository.Interface;

namespace TestProject.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private AdminContext _context;

        public Repository()
        {
            _context = new AdminContext();
        }

        public T Save(T item)
        {
            throw new NotImplementedException();
        }
    }
}
