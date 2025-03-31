
using RestaurantReservation.Domain.Restaurants.Models;

namespace RestaurantReservation.Domain.MenuItems.Models;

public class MenuItem
{
    public int MenuItemId { get; set; }
    public virtual Restaurant  Restaurant { get; set; }
    public string Name { get; set; }
    
    public string? Description { get; set; }
    public decimal Price { get; set; }
}