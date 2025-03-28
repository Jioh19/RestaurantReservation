using RestaurantReservation.Domain.Models.Restaurants;

namespace RestaurantReservation.Domain.Models.Employees;

public class Employee
{
    public long EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public virtual Restaurant Restaurant { get; set; }
}