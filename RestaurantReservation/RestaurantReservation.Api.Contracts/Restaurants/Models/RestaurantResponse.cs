namespace RestaurantReservation.Api.Contracts.Restaurants.Models;

public class RestaurantResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; } 
    public string PhoneNumber { get; set; }
    public TimeOnly OpeningHours { get; set; }
}