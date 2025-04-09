using RestaurantReservation.Domain.EntityReferences;

namespace RestaurantReservation.Domain.Orders.Models;

public class Order
{
    public long Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    
    public EntityReference<long> Reservation { get; set; } = EntityReference<long>.Empty;
    
    public EntityReference<long> Employee { get; set; } = EntityReference<long>.Empty;
    
    public IReadOnlyList<OrderItemReference> Items { get; set; } = Array.Empty<OrderItemReference>();
}