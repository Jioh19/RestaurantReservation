
using RestaurantReservation.Domain.Employees.Models;
using RestaurantReservation.Domain.EntityReferences;
using RestaurantReservation.Domain.Models.Reservations;

namespace RestaurantReservation.Domain.Models.Orders;

public class Order
{
    public long OrderId { get; set; }
    public virtual Reservation Reservation { get; set; }
    public virtual Employee Employee { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    
    public IReadOnlyList<OrderItemReference> OrderItems { get; set; } = Array.Empty<OrderItemReference>();

    
}