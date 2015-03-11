namespace TestProject.Service.Interface
{
    public interface IBaseService<T> where T : class
    {
        void Save(int id, T item, int modifiedId);

        object Create(T item, int createdId);

        void Remove(int id, int modifiedId);

        T Get(int id);
    }
}
