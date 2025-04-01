using RestaurantReservation.Domain.Errors;
using RestaurantReservation.Domain.MenuItems.Models;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Domain.MenuItems.Service;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _menuItemRepository;

    public MenuItemService(IMenuItemRepository menuItemRepository)
    {
        _menuItemRepository = menuItemRepository;
    }

    public async Task<MenuItem> GetMenuItemByIdAsync(long id)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(id);
        if (menuItem is null)
        {
            throw new EntityNotFoundException<MenuItem>(id.ToString());
        }

        return menuItem;
    }

    public async Task<IReadOnlyCollection<MenuItem>> GetAllMenuItemsAsync()
    {
        var menuItems = await _menuItemRepository.GetAllAsync();
        return menuItems.ToList();
    }

    public async Task<MenuItem> AddMenuItemAsync(MenuItem domainMenuItem)
    {
        var menuItem = await _menuItemRepository.AddAsync(domainMenuItem);
        return menuItem;
    }

    public async Task UpdateMenuItemAsync(MenuItem domainMenuItem)
    {
        await _menuItemRepository.UpdateAsync(domainMenuItem);
    }

    public async Task DeleteMenuItemAsync(long id)
    {
        await _menuItemRepository.DeleteAsync(id);
    }
}