namespace back_end_api.Repository.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> Get(int id);
        void Update(T entity);
        Task Add(T entity);
        void Delete(T entity);
    }
}
