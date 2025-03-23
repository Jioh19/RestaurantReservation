using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Domain.Models.Employees;

public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long EmployeeId { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Position { get; set; }
    
    [Required]
    [ForeignKey(nameof(Restaurant))]
    public long RestaurantId { get; set; }
    public virtual Restaurant Restaruant { get; set; }
}