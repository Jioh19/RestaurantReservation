using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Domain.Models.Reservations;

public class Reservation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ReservationId { get; set; }
    
    [Required]
    [ForeignKey(nameof(Customer))]
    public long CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    
    [Required]
    [ForeignKey(nameof(Restaurant))]
    public long RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
    
    [Required]
    [ForeignKey(nameof(Table))]
    public long TableId { get; set; }
    public virtual Table Table { get; set; }
    
    [Required]
    public DateTime ReservationDate { get; set; }
    
    [Required]
    public int PartySize { get; set; }
}