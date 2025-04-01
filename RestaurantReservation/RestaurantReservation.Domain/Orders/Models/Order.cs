using RestaurantReservation.Domain.EntityReferences;

namespace RestaurantReservation.Domain.Orders.Models;

public class Order
{
    public long Id { get; set; }
    public long ReservationId { get; set; }
    public long EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    
    public IReadOnlyList<OrderItemReference> OrderItems { get; set; } = Array.Empty<OrderItemReference>();
}