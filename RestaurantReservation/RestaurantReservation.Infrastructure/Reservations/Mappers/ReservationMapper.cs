using RestaurantReservation.Infrastructure.Reservations.Models;
using Riok.Mapperly.Abstractions;

namespace RestaurantReservation.Infrastructure.Reservations.Mappers;
using DomainReservation = RestaurantReservation.Domain.Reservations.Models.Reservation;

[Mapper]
public static partial class ReservationMapper
{

    public static partial DomainReservation ToDomain(this Reservation source);
    
    public static partial Reservation ToEntity(this DomainReservation source);
    
    public static partial void UpdateDomainToInfrastructure(this DomainReservation source, Reservation domain);
}