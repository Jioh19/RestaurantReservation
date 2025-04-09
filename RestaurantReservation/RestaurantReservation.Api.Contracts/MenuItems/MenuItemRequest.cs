using RestaurantReservation.Api.Contracts.EntityReferences;

namespace RestaurantReservation.Api.Contracts.MenuItems;

public record MenuItemRequest(
    long Id,
    string Name,
    string Description,
    decimal Price,
    EntityReference<long> Restaurant
    );