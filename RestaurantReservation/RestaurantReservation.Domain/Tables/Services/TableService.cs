using RestaurantReservation.Domain.Errors;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Domain.Tables.Models;

namespace RestaurantReservation.Domain.Tables.Services;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;

    public TableService(ITableRepository tableRepository)
    {
        _tableRepository = tableRepository;
    }

    public async Task<Table> GetTableByIdAsync(long id)
    {
        var table = await _tableRepository.GetByIdAsync(id);
        if (table is null)
        {
            throw new EntityNotFoundException<Table>(id.ToString());
        }
        return table;
    }

    public async Task<IReadOnlyCollection<Table>> GetAllTablesAsync()
    {
        Console.WriteLine("Entre al servicio de table");
        var tables = await _tableRepository.GetAllAsync();
        Console.WriteLine("Recibi las tablas");
        return tables.ToList();
    }

    public async Task<Table> AddTableAsync(Table domainTable)
    {
        var table = await _tableRepository.AddAsync(domainTable);
        return table;
    }

    public async Task UpdateTableAsync(Table domainTable)
    {
        await _tableRepository.UpdateAsync(domainTable);
    }

    public async Task DeleteTableAsync(long id)
    {
        await _tableRepository.DeleteAsync(id);
    }
}