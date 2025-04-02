namespace RestaurantReservation.Domain.MenuItems.Models;

public class MenuItem
{
    public long Id { get; set; }
    
    public long  RestaurantId { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public decimal Price { get; set; }
}