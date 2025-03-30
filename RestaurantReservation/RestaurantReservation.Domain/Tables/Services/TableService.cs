using RestaurantReservation.Domain.Tables.Models;

namespace RestaurantReservation.Domain.Tables.Services;

public class TableService : ITableService
{
    public async Task<Table> GetRestaurantByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<Table>> GetAllRestaurantsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Table> AddRestaurantAsync(Table domainTable)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateRestaurantAsync(Table domainTable)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteRestaurantAsync(long id)
    {
        throw new NotImplementedException();
    }
}