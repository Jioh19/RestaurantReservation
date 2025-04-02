namespace RestaurantReservation.Api.Contracts.MenuItems;

public record MenuItemResponse(
    long Id,
    long RestaurantId,
    string Name,
    string Description,
    decimal Price
);