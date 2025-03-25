using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantReservation.Domain.Models.MenuItems;
using RestaurantReservation.Domain.Models.Orders;

namespace RestaurantReservation.Domain.Models.OrderItems;

public class OrderItem
{
    public long OrderItemId { get; set; }
    public long OrderId { get; set; }
    public virtual Order Order { get; set; }
    public long ItemId { get; set; }
    public virtual MenuItem MenuItem { get; set; }
    public int  Quantity { get; set; }
}