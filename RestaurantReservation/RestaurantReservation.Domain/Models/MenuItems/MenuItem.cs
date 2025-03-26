
using RestaurantReservation.Domain.Models.Restaurants;

namespace RestaurantReservation.Domain.Models.MenuItems;

public class MenuItem
{
    public int ItemId { get; set; }
    public long RestaurantId { get; set; }
    public virtual Restaurant  Restaurant { get; set; }
    public string Name { get; set; }
    
    public string? Description { get; set; }
    public decimal Price { get; set; }
}