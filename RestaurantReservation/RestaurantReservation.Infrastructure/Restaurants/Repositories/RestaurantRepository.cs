using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Domain.Repositories;
using DomainRestaurant = RestaurantReservation.Domain.Restaurants.Models.Restaurant;
using RestaurantReservation.Infrastructure.Contexts;
using RestaurantReservation.Infrastructure.Customers.Repositories;
using RestaurantReservation.Infrastructure.Restaurants.Mappers;

namespace RestaurantReservation.Infrastructure.Restaurants.Repositories;

public class RestaurantRepository :  IRestaurantRepository
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

    public async Task<DomainRestaurant?> GetByIdAsync(long id)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);
        return restaurant?.ToDomain();
    }

    public async Task<DomainRestaurant> AddAsync(DomainRestaurant domainRestaurant)
    {
        var response = await _context.Restaurants.AddAsync(domainRestaurant.ToEntity());
        await _context.SaveChangesAsync();
        return response.Entity.ToDomain();
    }

    public async Task<DomainRestaurant?> UpdateAsync(DomainRestaurant domainRestaurant)
    {
        _logger.LogInformation($"Finding restaurant id {domainRestaurant.Id}");
        var restaurant = await _context.Restaurants.FindAsync(domainRestaurant.Id);
        if (restaurant is null)
        {
            _logger.LogError("Restaurant not found");
            return null;
        }
        RestaurantMapper.UpdateDomainToInfrastructure(domainRestaurant, restaurant);
        _context.Restaurants.Update(restaurant);
        await _context.SaveChangesAsync();
        return restaurant.ToDomain();
    }

    public async Task DeleteAsync(long id)
    {
        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
        if (restaurant is null)
        {
            return;
        }
        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync();
    }
}