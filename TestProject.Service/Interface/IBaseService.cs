namespace TestProject.Service.Interface
{
    public interface IBaseService<T> where T : class
    {
        void Save(T item);
    }
}
