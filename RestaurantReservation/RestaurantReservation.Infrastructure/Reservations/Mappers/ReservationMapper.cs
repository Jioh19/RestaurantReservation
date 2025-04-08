using RestaurantReservation.Domain.EntityReferences;
using RestaurantReservation.Infrastructure.Customers.Models;
using RestaurantReservation.Infrastructure.Reservations.Models;
using RestaurantReservation.Infrastructure.Restaurants.Models;
using RestaurantReservation.Infrastructure.Tables.Models;
using Riok.Mapperly.Abstractions;

namespace RestaurantReservation.Infrastructure.Reservations.Mappers;
using DomainReservation = RestaurantReservation.Domain.Reservations.Models.Reservation;

[Mapper]
public static partial class ReservationMapper
{
    [MapperIgnoreSource(nameof(Reservation.CustomerId))]
    [MapperIgnoreSource(nameof(Reservation.RestaurantId))]
    [MapperIgnoreSource(nameof(Reservation.TableId))]
    public static partial DomainReservation ToDomain(this Reservation source);
    
    [MapProperty(nameof(DomainReservation.Customer.Id), nameof(Reservation.CustomerId))]
    [MapperIgnoreTarget(nameof(Reservation.Customer))]
    [MapProperty(nameof(DomainReservation.Restaurant.Id), nameof(Reservation.RestaurantId))]
    [MapperIgnoreTarget(nameof(Reservation.Restaurant))]
    [MapProperty(nameof(DomainReservation.Table.Id), nameof(Reservation.TableId))]
    [MapperIgnoreTarget(nameof(Reservation.Table))]
    public static partial Reservation ToEntity(this DomainReservation source);
    
    [MapperIgnoreTarget(nameof(Reservation.Customer))]
    [MapperIgnoreTarget(nameof(Reservation.Restaurant))]
    [MapperIgnoreTarget(nameof(Reservation.Table))]
    public static partial void UpdateDomainToInfrastructure(this DomainReservation source, Reservation domain);
    
    private static EntityReference<long> ToDomain(Restaurant source) =>
        new() { Id = source.Id, Name = source.Name };
    private static EntityReference<long> ToDomain(Customer source) =>
        new() { Id = source.Id, Name = source.FirstName };
    private static EntityReference<long> ToDomain(Table source) =>
        new() { Id = source.Id, Name = string.Empty };
}