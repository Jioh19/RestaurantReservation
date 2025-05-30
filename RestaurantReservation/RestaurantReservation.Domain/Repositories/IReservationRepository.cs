using RestaurantReservation.Domain.Reservations.Models;

namespace RestaurantReservation.Domain.Repositories;

public interface IReservationRepository: IRepository<Reservation>
{
    Task AddAllAsync(IEnumerable<Reservation> domainReservations);
    Task<IReadOnlyCollection<Reservation>> GetReservationsByCustomerIdAsync(long customerId);
}