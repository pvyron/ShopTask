namespace Shop.Api.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
