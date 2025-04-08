
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Contexts;
using RestaurantReservation.Infrastructure.MenuItems.Mappers;
using RestaurantReservation.Infrastructure.MenuItems.Models;
using DomainMenuItem = RestaurantReservation.Domain.MenuItems.Models.MenuItem;

namespace RestaurantReservation.Infrastructure.MenuItems.Repositories;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly RestaurantReservationDbContext _context;
    private readonly ILogger<MenuItemRepository> _logger;

    private IQueryable<MenuItem> FullQuery => _context.MenuItems.Include(m => m.Restaurant);
    public MenuItemRepository(RestaurantReservationDbContext context, ILogger<MenuItemRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<IReadOnlyCollection<DomainMenuItem>> GetAllAsync()
    {
        var menuItem = await FullQuery.ToListAsync();
        _logger.LogInformation("Getting all MenuItems" + " " + menuItem.Count);
        return menuItem.Select(t => t.ToDomain()).ToList();
    }

    public async Task<DomainMenuItem?> GetByIdAsync(long id)
    {
        var menuItem = await FullQuery.FirstOrDefaultAsync(m => m.Id == id);
        return menuItem?.ToDomain();
    }

    public async Task<DomainMenuItem> AddAsync(DomainMenuItem domainMenuItem)
    {
        var entity = domainMenuItem.ToEntity();
        await _context.MenuItems.AddAsync(entity);
        await _context.SaveChangesAsync();
        return (await FullQuery
                .FirstAsync(m => m.Id == entity.Id))
            .ToDomain();
    }

    public async Task<DomainMenuItem?> UpdateAsync(DomainMenuItem domainMenuItem)
    {
        var menuItem = await FullQuery.FirstOrDefaultAsync(m => m.Id == domainMenuItem.Id);
        if (menuItem is null)
        {
            _logger.LogError("MenuItem not found");
            return null;
        }
        
        var restaurant =  await _context.Restaurants.FindAsync(domainMenuItem.Restaurant.Id);
        if (restaurant is null)
        {
            _logger.LogError("Restaurant not found");
            return null;
        }

        menuItem.Restaurant = restaurant;
        _logger.LogInformation("Updating MenuItem" + " " + menuItem.Restaurant.Name);
        MenuItemMapper.UpdateDomainToInfrastructure(domainMenuItem, menuItem);
        _context.MenuItems.Update(menuItem);
        await _context.SaveChangesAsync();
        return menuItem.ToDomain();
    }

    public async Task DeleteAsync(long id)
    {
        var menuItem = await _context.MenuItems.FirstOrDefaultAsync(t => t.Id == id);
        if (menuItem is null)
        {
            return;
        }
        _context.MenuItems.Remove(menuItem);
        await _context.SaveChangesAsync();
    }
}