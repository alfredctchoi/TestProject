using TestProject.Database.Context.Interface;
using TestProject.Model.Domain;
using TestProject.Repository.Interface;

namespace TestProject.Repository
{
    public class SessionRepository : BaseRepository<Session>, ISessionRepository
    {
        public SessionRepository(IAdminContext context) : base(context)
        {

        }
    }
}
