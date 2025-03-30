using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DomainRestaurant = RestaurantReservation.Domain.Restaurants.Models.Restaurant;
using RestaurantReservation.Infrastructure.Contexts;
using RestaurantReservation.Infrastructure.Customers.Repositories;
using RestaurantReservation.Infrastructure.Restaurants.Mappers;

namespace RestaurantReservation.Infrastructure.Restaurants.Repositories;

public class RestaurantRepository
{
    private readonly RestaurantReservationDbContext _context;
    private readonly ILogger<CustomerRepository> _logger;

    public RestaurantRepository(RestaurantReservationDbContext context, ILogger<CustomerRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<DomainRestaurant>> GetAllAsync()
    {
        var restaurants = await _context.Restaurants.ToListAsync();
        _logger.LogInformation("Getting all Restaurants" + " " + restaurants.Count);
        return restaurants.Select(r => r.ToDomain()).ToList();
    }

    public async Task<DomainRestaurant?> GetByIdAsync(int id)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);
        return restaurant?.ToDomain();
    }
}