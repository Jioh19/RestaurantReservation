using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Domain.Models.Restaurants;

public class Restaurant
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long RestaurantId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Address { get; set; } 
    
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
     
    public TimeOnly OpeningHours { get; set; }
}