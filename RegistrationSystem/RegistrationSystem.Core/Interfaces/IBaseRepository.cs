

namespace RegistrationSystem.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T?> GetByIdAsync<TId>(TId id) where TId : notnull;
        Task<IList<T>> GetAll();
        Task RemoveAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }
}