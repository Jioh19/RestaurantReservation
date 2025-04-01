using RestaurantReservation.Domain.Errors;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Domain.Reservations.Models;

namespace RestaurantReservation.Domain.Reservations.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Reservation> GetReservationByIdAsync(long id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);
        if (reservation is null)
        {
            throw new EntityNotFoundException<Reservation>(id.ToString());
        }

        return reservation;
    }

    public async Task<IReadOnlyCollection<Reservation>> GetAllReservationsAsync()
    {
        var reservations = await _reservationRepository.GetAllAsync();
        return reservations.ToList();
    }

    public async Task<Reservation> AddReservationAsync(Reservation domainReservation)
    {
        var reservation = await _reservationRepository.AddAsync(domainReservation);
        return reservation;
    }

    public async Task UpdateReservationAsync(Reservation domainReservation)
    {
        await _reservationRepository.UpdateAsync(domainReservation);
    }

    public async Task DeleteReservationAsync(long id)
    {
        await _reservationRepository.DeleteAsync(id);
    }
}