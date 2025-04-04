using RestaurantReservation.Infrastructure.Reservations.Models;
using Riok.Mapperly.Abstractions;

namespace RestaurantReservation.Infrastructure.Reservations.Mappers;
using DomainReservation = RestaurantReservation.Domain.Reservations.Models.Reservation;

[Mapper]
public static partial class ReservationMapper
{
    [MapperIgnoreSource(nameof(Reservation.Customer))]
    [MapperIgnoreSource(nameof(Reservation.Restaurant))]
    [MapperIgnoreSource(nameof(Reservation.Table))]
    public static partial DomainReservation ToDomain(this Reservation source);
    
    [MapperIgnoreTarget(nameof(Reservation.Customer))]
    [MapperIgnoreTarget(nameof(Reservation.Restaurant))]
    [MapperIgnoreTarget(nameof(Reservation.Table))]
    public static partial Reservation ToEntity(this DomainReservation source);
    
    [MapperIgnoreTarget(nameof(Reservation.Customer))]
    [MapperIgnoreTarget(nameof(Reservation.Restaurant))]
    [MapperIgnoreTarget(nameof(Reservation.Table))]
    public static partial void UpdateDomainToInfrastructure(this DomainReservation source, Reservation domain);
}