namespace RestaurantReservation.Api.Contracts.MenuItem;

public record MenuItemResponse(
    long Id,
    long RestaurantId,
    string Name,
    string Description,
    decimal Price
);