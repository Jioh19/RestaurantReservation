namespace RestaurantReservation.Domain.Restaurants.Models;

public class Restaurant
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public TimeOnly OpeningHours { get; set; }
}