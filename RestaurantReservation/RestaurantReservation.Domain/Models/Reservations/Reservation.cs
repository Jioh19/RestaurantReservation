namespace RestaurantReservation.Domain.Models.Reservations;

public record Reservation(
    long ReservationId,
    long CustomerId,
    long RestaurantId,
    long TableId,
    DateTime ReservationDate,
    int PartySize);