using RestaurantReservation.Api.Contracts.EntityReferences;

namespace RestaurantReservation.Api.Contracts.Orders.Models;

public record OrderRequest(
    long Id, 
    DateTime OrderDate,
    decimal TotalAmount,
    EntityReference<long> Reservation,
    EntityReference<long> Employee
);