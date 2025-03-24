using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantReservation.Domain.Models.Reservations;

namespace RestaurantReservation.Domain.Models.Orders;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long OrderId { get; set; }
    
    [Required]
    [ForeignKey(nameof(Reservation))]
    public long ReservationId { get; set; }
    public virtual Reservation Reservation { get; set; }
    
    [Required]
    [ForeignKey(nameof(Employee))]
    public long EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
    
    [Required]
    public DateTime OrderDate { get; set; }
    
    [Required]
    public Decimal TotalAmount { get; set; }
    
}