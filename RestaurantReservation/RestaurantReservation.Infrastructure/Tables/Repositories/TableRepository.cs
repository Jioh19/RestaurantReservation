using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Contexts;
using RestaurantReservation.Infrastructure.Tables.Mappers;
using RestaurantReservation.Infrastructure.Tables.Models;
using DomainTable = RestaurantReservation.Domain.Tables.Models.Table;

namespace RestaurantReservation.Infrastructure.Tables.Repositories;


public class TableRepository : ITableRepository
{
    private readonly RestaurantReservationDbContext _context;
    private readonly ILogger<TableRepository> _logger;

    private IQueryable<Table> FullQuery => _context.Tables.Include(t => t.Restaurant);

    public TableRepository(RestaurantReservationDbContext context, ILogger<TableRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<IReadOnlyCollection<DomainTable>> GetAllAsync()
    {
        var tables = await FullQuery.ToListAsync();
        _logger.LogInformation("Getting all Tables" + " " + tables.Count);
        return tables.Select(t => t.ToDomain()).ToList();
    }

    public async Task<DomainTable?> GetByIdAsync(long id)
    {
        var table = await FullQuery.FirstOrDefaultAsync(t => t.Id == id);
        return table?.ToDomain();
    }

    public async Task<DomainTable> AddAsync(DomainTable domainTable)
    {
        var entity = domainTable.ToEntity();
        await _context.Tables.AddAsync(entity);
        await _context.SaveChangesAsync();
        return (await FullQuery
            .FirstAsync(t => t.Id == entity.Id))
            .ToDomain();
    }

    public async Task<DomainTable?> UpdateAsync(DomainTable domainTable)
    {
        var table = await _context.Tables.FindAsync(domainTable.Id);
        if (table is null)
        {
            _logger.LogError("Table not found");
            return null;
        }
        
        var restaurant =  await _context.Restaurants.FindAsync(domainTable.Restaurant.Id);
        if (restaurant is null)
        {
            _logger.LogError("Restaurant not found");
            return null;
        }

        table.Restaurant = restaurant;
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