namespace RestaurantReservation.Domain.Models.Orders;

public record Order(
    long OrderId,
    long ReservationId,
    long EmployeeId,
    DateTime OrderDate,
    decimal TotalPrice);