
namespace RestaurantReservation.Domain.Restaurants.Models;

public class Restaurant
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; } 
    public string PhoneNumber { get; set; }
    public TimeOnly OpeningHours { get; set; }
}