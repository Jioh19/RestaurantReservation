using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Contexts;
using RestaurantReservation.Infrastructure.Reservations.Mappers;
using DomainReservation = RestaurantReservation.Domain.Reservations.Models.Reservation;

namespace RestaurantReservation.Infrastructure.Reservations.Repositories;

public class ReservationRepository :  IReservationRepository
{
    private readonly RestaurantReservationDbContext _context;
    private readonly ILogger<ReservationRepository> _logger;

    public ReservationRepository(RestaurantReservationDbContext context, ILogger<ReservationRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<IReadOnlyCollection<DomainReservation>> GetAllAsync()
    {
        var reservations = await _context.Reservations.ToListAsync();
        _logger.LogInformation("Getting all Reservations" + " " + reservations.Count);
        return reservations.Select(t => t.ToDomain()).ToList();
    }

    public async Task<DomainReservation?> GetByIdAsync(long id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        return reservation?.ToDomain();
    }

    public async Task<DomainReservation> AddAsync(DomainReservation domainReservation)
    {
        var response = await _context.Reservations.AddAsync(domainReservation.ToEntity());
        await _context.SaveChangesAsync();
        return response.Entity.ToDomain();
    }

    public async Task<DomainReservation?> UpdateAsync(DomainReservation domainReservation)
    {
        var reservation = await _context.Reservations.FindAsync(domainReservation.Id);
        if (reservation is null)
        {
            _logger.LogError("Reservation not found");
            return null;
        }
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
        var reservations = await _context.Reservations.Where(t => t.CustomerId == customerId).ToListAsync();
        _logger.LogInformation($"Getting all Reservations by Customer Id {customerId}");
        return reservations.Select(t => t.ToDomain()).ToList();
    }
}