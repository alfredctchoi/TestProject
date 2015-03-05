using TestProject.Database.Context;
using TestProject.Model.Domain;
using TestProject.Repository.Interface;

namespace TestProject.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly AdminContext _context;

        public UserRepository()
        {
            _context = new AdminContext();
        }

        public User Save(User item)
        {
            _context.Users.Add(item);
            _context.SaveChanges();
            return item;
        }
    }
}
