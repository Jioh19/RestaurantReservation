namespace RestaurantReservation.Api.Contracts.Reservations;

public record ReservationRequest(
    long Id, 
    long CustomerId, 
    long RestaurantId,
    long TableId,
    DateTime ReservationDate,
    int PartySize
    );