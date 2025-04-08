using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Contexts;
using RestaurantReservation.Infrastructure.Reservations.Mappers;
using RestaurantReservation.Infrastructure.Reservations.Models;
using DomainReservation = RestaurantReservation.Domain.Reservations.Models.Reservation;

namespace RestaurantReservation.Infrastructure.Reservations.Repositories;

public class ReservationRepository :  IReservationRepository
{
    private readonly RestaurantReservationDbContext _context;
    private readonly ILogger<ReservationRepository> _logger;

    private IQueryable<Reservation> FullQuery => _context.Reservations
        .Include(r => r.Restaurant)
        .Include( r => r.Table)
        .Include( r => r.Customer);
    
    public ReservationRepository(RestaurantReservationDbContext context, ILogger<ReservationRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<IReadOnlyCollection<DomainReservation>> GetAllAsync()
    {
        var reservations = await FullQuery.ToListAsync();
        _logger.LogInformation("Getting all Reservations" + " " + reservations.Count);
        return reservations.Select(t => t.ToDomain()).ToList();
    }

    public async Task<DomainReservation?> GetByIdAsync(long id)
    {
        var reservation = await FullQuery.FirstOrDefaultAsync(r => r.Id == id);
        return reservation?.ToDomain();
    }

    public async Task<DomainReservation> AddAsync(DomainReservation domainReservation)
    {
        var entity = domainReservation.ToEntity();
        _logger.LogInformation($"Adding Reservation {entity.ReservationDate} {entity.CustomerId} {entity.RestaurantId}");
        await _context.Reservations.AddAsync(entity);
        await _context.SaveChangesAsync();
        return (await FullQuery
                .FirstAsync(r => r.Id == entity.Id))
            .ToDomain();
    }

    public async Task<DomainReservation?> UpdateAsync(DomainReservation domainReservation)
    {
        var reservation = await FullQuery.FirstOrDefaultAsync(r => r.Id == domainReservation.Id);
        if (reservation is null)
        {
            _logger.LogError("Reservation not found");
            return null;
        }
        
        var restaurant = await _context.Restaurants.FindAsync(domainReservation.Restaurant.Id);
        if (restaurant is null)
        {
            _logger.LogError("Restaurant not found");
            return null;
        }
        
        var customer = await _context.Customers.FindAsync(domainReservation.Customer.Id);
        if (customer is null)
        {
            _logger.LogError("Customer not found");
            return null;
        }
        
        var table = await _context.Tables.FindAsync(domainReservation.Table.Id);
        if (table is null)
        {
            _logger.LogError("Table not found");
            return null;
        }
        
        reservation.Restaurant = restaurant;
        reservation.Customer = customer;
        reservation.Table = table;
        _logger.LogInformation("Updating Reservation" + " " + reservation.Restaurant.Name);
        ReservationMapper.UpdateDomainToInfrastructure(domainReservation, reservation);
        _context.Reservations.Update(reservation);
        await _context.SaveChangesAsync();
        return reservation.ToDomain();
    }

    public async Task DeleteAsync(long id)
    {
        var reservation = await _context.Reservations.FirstOrDefaultAsync(t => t.Id == id);
        if (reservation is null)
        {
            return;
        }
        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
    }
    
    public async Task AddAllAsync(IEnumerable<DomainReservation> domainReservations)
    {
        await _context.Reservations.AddRangeAsync(domainReservations.Select(t => t.ToEntity()).ToList());
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<DomainReservation>> GetReservationsByCustomerIdAsync(long customerId)
    {
        var reservations = await FullQuery.Where(t => t.CustomerId == customerId).ToListAsync();
        _logger.LogInformation($"Getting all Reservations by Customer Id {customerId}");
        return reservations.Select(t => t.ToDomain()).ToList();
    }
}