using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Domain.Restaurants.Models;
using RestaurantReservation.Infrastructure.Contexts;
using RestaurantReservation.Infrastructure.Tables.Mappers;
using DomainTable = RestaurantReservation.Domain.Tables.Models.Table;

namespace RestaurantReservation.Infrastructure.Tables.Repositories;


public class TableRepository : ITableRepository
{
    private readonly RestaurantReservationDbContext _context;
    private readonly ILogger<TableRepository> _logger;

    public TableRepository(RestaurantReservationDbContext context, ILogger<TableRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<IEnumerable<DomainTable>> GetAllAsync()
    {
        var tables = await _context.Tables.ToListAsync();
        _logger.LogInformation("Getting all Tables" + " " + tables.Count);
        return tables.Select(t => t.ToDomain()).ToList();
    }

    public async Task<DomainTable?> GetByIdAsync(long id)
    {
        var table = await _context.Tables.FindAsync(id);
        return table?.ToDomain();
    }

    public async Task<DomainTable> AddAsync(DomainTable domainTable)
    {
        var response = await _context.Tables.AddAsync(domainTable.ToEntity());
        await _context.SaveChangesAsync();
        return response.Entity.ToDomain();
    }

    public async Task<DomainTable?> UpdateAsync(DomainTable domainTable)
    {
        _logger.LogInformation("Entre al repositorio de Table");
        var table = await _context.Tables.FindAsync(domainTable.Id);
        if (table is null)
        {
            _logger.LogError("Table not found");
            return null;
        }
        table.Restaurant = await _context.Restaurants.FindAsync(domainTable.RestaurantId);
        _logger.LogInformation("Updating Table" + " " + table.Restaurant.Name);
        TableMapper.UpdateDomainToInfrastructure(domainTable, table);
        _context.Tables.Update(table);
        await _context.SaveChangesAsync();
        return table.ToDomain();
    }

    public async Task DeleteAsync(long id)
    {
        var table = await _context.Tables.FirstOrDefaultAsync(t => t.Id == id);
        if (table is null)
        {
            return;
        }
        _context.Tables.Remove(table);
        await _context.SaveChangesAsync();
    }
}