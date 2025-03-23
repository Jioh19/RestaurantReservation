namespace RestaurantReservation.Domain.Models.Customers;

public record Customer(
    long CustomerId,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber);