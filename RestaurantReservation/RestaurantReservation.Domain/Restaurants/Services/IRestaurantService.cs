using RestaurantReservation.Domain.Restaurants.Models;

namespace RestaurantReservation.Domain.Restaurants.Services;

public interface IRestaurantService
{
    Task<Restaurant> GetRestaurantByIdAsync(long id);
    Task<IReadOnlyCollection<Restaurant>> GetAllRestaurantsAsync();
    Task<Restaurant> AddRestaurantAsync(Restaurant domainRestaurant);
    Task UpdateRestaurantAsync(Restaurant domainRestaurant);
    Task DeleteRestaurantAsync(long id);
}