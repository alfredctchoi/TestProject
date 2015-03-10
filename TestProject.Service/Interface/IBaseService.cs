namespace TestProject.Service.Interface
{
    public interface IBaseService<T> where T : class
    {
        void Save(object id, T item);

        void Create(T item);

        void Remove(object id);

        T Get(object id);
    }
}
