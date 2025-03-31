
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Contexts;
using RestaurantReservation.Infrastructure.MenuItems.Mappers;
using DomainMenuItem = RestaurantReservation.Domain.MenuItems.Models.MenuItem;

namespace RestaurantReservation.Infrastructure.MenuItems.Repositories;


public class MenuItemRepository : IMenuItemRepository
{
    private readonly RestaurantReservationDbContext _context;
    private readonly ILogger<MenuItemRepository> _logger;

    public MenuItemRepository(RestaurantReservationDbContext context, ILogger<MenuItemRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<IEnumerable<DomainMenuItem>> GetAllAsync()
    {
        var menuItem = await _context.MenuItems.ToListAsync();
        _logger.LogInformation("Getting all MenuItems" + " " + menuItem.Count);
        return menuItem.Select(t => t.ToDomain()).ToList();
    }

    public async Task<DomainMenuItem?> GetByIdAsync(long id)
    {
        var table = await _context.MenuItems.FindAsync(id);
        return table?.ToDomain();
    }

    public async Task<DomainMenuItem> AddAsync(DomainMenuItem domainMenuItem)
    {
        var response = await _context.MenuItems.AddAsync(domainMenuItem.ToEntity());
        await _context.SaveChangesAsync();
        return response.Entity.ToDomain();
    }

    public async Task<DomainMenuItem?> UpdateAsync(DomainMenuItem domainMenuItem)
    {
        var table = await _context.MenuItems.FindAsync(domainMenuItem.Id);
        if (table is null)
        {
            _logger.LogError("MenuItem not found");
            return null;
        }
        MenuItemMapper.UpdateDomainToInfrastructure(domainMenuItem, table);
        _context.MenuItems.Update(table);
        await _context.SaveChangesAsync();
        return table.ToDomain();
    }

    public async Task DeleteAsync(long id)
    {
        var table = await _context.MenuItems.FirstOrDefaultAsync(t => t.Id == id);
        if (table is null)
        {
            return;
        }
        _context.MenuItems.Remove(table);
        await _context.SaveChangesAsync();
    }
}