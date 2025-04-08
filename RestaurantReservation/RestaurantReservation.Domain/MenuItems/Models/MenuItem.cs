using RestaurantReservation.Domain.EntityReferences;

namespace RestaurantReservation.Domain.MenuItems.Models;

public class MenuItem
{
    public long Id { get; set; }
    

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public decimal Price { get; set; }
    public EntityReference<long> Restaurant { get; set; } = EntityReference<long>.Empty;
}
