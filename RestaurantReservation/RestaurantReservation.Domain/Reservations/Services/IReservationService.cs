using RestaurantReservation.Domain.Reservations.Models;

namespace RestaurantReservation.Domain.Reservations.Services;

public interface IReservationService
{
    Task<Reservation> GetReservationByIdAsync(long id);
    Task<IReadOnlyCollection<Reservation>> GetAllReservationsAsync();
    Task<Reservation> AddReservationAsync(Reservation domainReservation);
    Task UpdateReservationAsync(Reservation domainReservation);
    Task DeleteReservationAsync(long id);
    Task AddAllReservationAsync(IEnumerable<Reservation> domainReservations);
    Task<IReadOnlyCollection<Reservation?>> GetReservationsByCustomerIdAsync(long customerId);
}