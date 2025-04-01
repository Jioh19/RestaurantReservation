using RestaurantReservation.Domain.Tables.Models;

namespace RestaurantReservation.Domain.Tables.Services;

public interface ITableService
{
    Task<Table> GetTableByIdAsync(long id);
    Task<IReadOnlyCollection<Table>> GetAllTablesAsync();
    Task<Table> AddTableAsync(Table domainTable);
    Task UpdateTableAsync(Table domainTable);
    Task DeleteTableAsync(long id);
}