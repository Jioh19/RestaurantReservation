namespace RestaurantReservation.Domain.Models.MenuItems;

public record MenuItem(
    long ItemId,
    long RestaurantId,
    string Name,
    string Description,
    decimal Price);