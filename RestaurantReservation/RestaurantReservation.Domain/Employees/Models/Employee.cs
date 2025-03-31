using RestaurantReservation.Domain.Restaurants.Models;

namespace RestaurantReservation.Domain.Employees.Models;

public class Employee
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public long RestaurantId { get; set; }
}