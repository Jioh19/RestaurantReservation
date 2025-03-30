using RestaurantReservation.Domain.Tables.Models;

namespace RestaurantReservation.Domain.Tables.Services;

public interface ITableService
{
    Task<Table> GetRestaurantByIdAsync(long id);
    Task<IReadOnlyCollection<Table>> GetAllRestaurantsAsync();
    Task<Table> AddRestaurantAsync(Table domainTable);
    Task UpdateRestaurantAsync(Table domainTable);
    Task DeleteRestaurantAsync(long id);
}