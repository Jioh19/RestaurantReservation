namespace RestaurantReservation.Api.Contracts.Employees.Models;

public record EmployeeResponse(
    long Id,
    string FirstName,
    string LastName,
    string Position,
    long RestaurantId
);