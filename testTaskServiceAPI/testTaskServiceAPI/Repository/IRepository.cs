namespace testTaskServiceAPI.Repository
{
    public interface IRepository<T> where T : class
    {
        public T GetById(int id);
        public void Add(T value);
        public void Update(T value);
        public void Delete(T id);
    }
}
