namespace RestaurantReservation.Api.Contracts.Orders.Models;

public record OrderResponse(
    long Id, 
    long ReservationId,
    long EmployeeId,
    DateTime OrderDate,
    decimal TotalAmount
    );