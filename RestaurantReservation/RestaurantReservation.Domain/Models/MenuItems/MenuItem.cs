using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Domain.Models.MenuItems;

public class MenuItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ItemId { get; set; }
    
    [Required]
    [ForeignKey(nameof(Restaurant))]
    public long RestaurantId { get; set; }
    public virtual Restaurant  Restaurant { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
}