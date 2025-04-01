namespace RestaurantReservation.Domain.Repositories;
public interface IRepository<T> where T : class
{
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task<T?> GetByIdAsync(long id);
    Task<T> AddAsync(T entity);
    Task<T?> UpdateAsync(T entity);
    Task DeleteAsync(long id);
    
}