using RestaurantReservation.Domain.MenuItems.Models;

namespace RestaurantReservation.Domain.MenuItems.Service;

public interface IMenuItemService
{
    Task<MenuItem> GetMenuItemByIdAsync(long id);
    Task<IReadOnlyCollection<MenuItem>> GetAllMenuItemsAsync();
    Task<MenuItem> AddMenuItemAsync(MenuItem domainMenuItem);
    Task UpdateMenuItemAsync(MenuItem domainMenuItem);
    Task DeleteMenuItemAsync(long id);
}