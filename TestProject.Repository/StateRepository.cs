using TestProject.Database.Context.Interface;
using TestProject.Model.Domain;
using TestProject.Repository.Interface;

namespace TestProject.Repository
{
    public class StateRepository: BaseRepository<State>, IStateRepository
    {
        public StateRepository(ITestContext context) : base(context)
        {
        }
    }
}
