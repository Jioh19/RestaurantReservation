using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantReservation.Domain.Models.Orders;

namespace RestaurantReservation.Domain.Models.OrderItems;

public class OrderItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long OrderItemId { get; set; }
    
    [Required]
    [ForeignKey(nameof(Order))]
    public long OrderId { get; set; }
    public virtual Order Order { get; set; }
    
    [Required]
    [ForeignKey(nameof(Item))]
    public long ItemId { get; set; }
    public virtual Item Item { get; set; }
    
    [Required]
    public int  Quantity { get; set; }
}