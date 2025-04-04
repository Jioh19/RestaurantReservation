using RestaurantReservation.Api.Contracts.Reservations;
using Riok.Mapperly.Abstractions;
using DomainReservation = RestaurantReservation.Domain.Reservations.Models.Reservation;

namespace RestaurantReservation.Api.Reservations.Mappers;

[Mapper]
public static partial class ReservationMapperDto
{
    public static partial ReservationResponse ToResponse(this DomainReservation source);
    
    public static partial DomainReservation ToDomain(this ReservationRequest source);
}