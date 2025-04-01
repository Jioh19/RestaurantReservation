namespace RestaurantReservation.Api.Contracts.Orders.Models;

public record OrderRequest(
    long Id, 
    long ReservationId,
    long EmployeeId,
    DateTime OrderDate,
    decimal TotalAmount
);