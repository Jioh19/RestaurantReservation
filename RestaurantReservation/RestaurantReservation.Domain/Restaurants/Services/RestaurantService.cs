using RestaurantReservation.Domain.Errors;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Domain.Restaurants.Models;

namespace RestaurantReservation.Domain.Restaurants.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantService(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    public async Task<Restaurant> GetRestaurantByIdAsync(long id)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id);
        if (restaurant == null)
        {
            throw new EntityNotFoundException<Restaurant>(id.ToString());
        }

        return restaurant;
    }

    public async Task<IReadOnlyCollection<Restaurant>> GetAllRestaurantsAsync()
    {
        var restaurants = await _restaurantRepository.GetAllAsync();
        return restaurants.ToList();
    }

    public async Task<Restaurant> AddRestaurantAsync(Restaurant domainRestaurant)
    {
        var restaurant = await _restaurantRepository.AddAsync(domainRestaurant);
        return restaurant;
    }

    public async Task UpdateRestaurantAsync(Restaurant domainRestaurant)
    {
        await _restaurantRepository.UpdateAsync(domainRestaurant);
    }

    public async Task DeleteRestaurantAsync(long id)
    {
        await _restaurantRepository.DeleteAsync(id);
    }
}