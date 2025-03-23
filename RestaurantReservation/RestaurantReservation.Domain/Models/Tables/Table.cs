namespace RestaurantReservation.Domain.Models.Tables;

public record Table(
    long TableId,
    long RestaurantId,
    int Capacity);