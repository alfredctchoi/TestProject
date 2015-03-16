using TestProject.Database.Context.Interface;
using TestProject.Model.Domain;
using TestProject.Repository.Interface;

namespace TestProject.Repository
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(ITestContext context) : base(context)
        {
        }
    }
}
