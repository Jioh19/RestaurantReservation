namespace RestaurantReservation.Domain.Models.OrderItems;

public record OrdetItem(
    long OrderItemId,
    long OrderId,
    long ItemId,
    int Quantity);