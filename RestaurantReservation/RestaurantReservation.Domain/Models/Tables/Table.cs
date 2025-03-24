using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Domain.Models.Tables;

public class Table
{
    [Key]
    public long TableId { get; set; }
    
    [Required]
    [ForeignKey(nameof(Restaurant))]
    public long RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }

    public int Capacity { get; set; }
    
    
}