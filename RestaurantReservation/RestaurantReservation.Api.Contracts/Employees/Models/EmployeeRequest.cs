namespace RestaurantReservation.Api.Contracts.Employees.Models;

public record EmployeeRequest(
    long Id,
    string FirstName,
    string LastName,
    string Position,
    long RestaurantId
);