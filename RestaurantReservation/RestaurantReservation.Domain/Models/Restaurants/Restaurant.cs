
namespace RestaurantReservation.Domain.Models.Restaurants;

public class Restaurant
{
    public long RestaurantId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; } 
    public string PhoneNumber { get; set; }
    public TimeOnly OpeningHours { get; set; }
}