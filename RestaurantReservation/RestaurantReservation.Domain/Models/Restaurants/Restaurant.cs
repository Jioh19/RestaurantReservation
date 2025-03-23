namespace RestaurantReservation.Domain.Models.Restaurants;

public record Restaurant(
    long RestaurantId,
    string Name,
    string Address,
    int PhoneNumber,
    TimeOnly OpeningHours);