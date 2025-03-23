namespace RestaurantReservation.Domain.Models.Employees;

public record Employee(
    long EmployeeId,
    long RestaurantId,
    string FirstName,
    string LastName,
    string Position);