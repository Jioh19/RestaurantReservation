namespace RestaurantReservation.Api.Contracts.MenuItems;

public record MenuItemRequest(
    long Id,
    long RestaurantId,
    string Name,
    string Description,
    decimal Price
    );