using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Domain.Models.Customers;

public class DomainCustomer
{
    [Key]
    public long CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    
}