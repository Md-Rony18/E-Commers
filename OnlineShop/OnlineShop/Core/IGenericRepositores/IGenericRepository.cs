namespace OnlineShop.Core.IGenericRepositores
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(Guid id);
        Task<bool> Add(T entity);
        Task<bool> Add(T entity,IFormFile file);
        Task<bool> Update(T entity);
        Task<bool> Update(T entity, IFormFile file);
        Task<bool> Delete(Guid id);
    }
}
