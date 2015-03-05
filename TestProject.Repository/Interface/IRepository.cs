
namespace TestProject.Repository.Interface
{
    public interface IRepository<T> where T : class
    {

        T Save(T item);

    }
}
