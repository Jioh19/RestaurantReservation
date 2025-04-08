using RestaurantReservation.Domain.EntityReferences;
using RestaurantReservation.Infrastructure.Employees.Models;
using RestaurantReservation.Infrastructure.Orders.Models;
using RestaurantReservation.Infrastructure.Reservations.Models;
using Riok.Mapperly.Abstractions;
using DomainOrder = RestaurantReservation.Domain.Orders.Models.Order;

namespace RestaurantReservation.Infrastructure.Orders.Mappers;

[Mapper]
public static partial class OrderMapper
{

    [MapperIgnoreSource(nameof(Order.EmployeeId))]
    [MapperIgnoreSource(nameof(Order.ReservationId))]
    [MapperIgnoreSource(nameof(Order.OrderItems))]
    [MapperIgnoreTarget(nameof(DomainOrder.Items))]
    public static partial DomainOrder ToDomain(this Order source);
    
    [MapProperty(nameof(DomainOrder.Employee.Id), nameof(Order.EmployeeId))]
    [MapperIgnoreTarget(nameof(Order.Employee))]
    [MapProperty(nameof(DomainOrder.Reservation.Id), nameof(Order.ReservationId))]
    [MapperIgnoreTarget(nameof(Order.Reservation))]
    [MapperIgnoreTarget(nameof(Order.OrderItems))]
    [MapperIgnoreSource(nameof(DomainOrder.Items))]
    public static partial Order ToEntity(this DomainOrder source);
    
    [MapperIgnoreTarget(nameof(Order.Employee))]
    [MapperIgnoreTarget(nameof(Order.OrderItems))]
    [MapperIgnoreTarget(nameof(Order.Reservation))]
    [MapperIgnoreSource(nameof(DomainOrder.Items))]
    public static partial void UpdateDomainToInfrastructure(this DomainOrder source, Order domain);
    
    private static EntityReference<long> ToDomain(Employee source) =>
        new() { Id = source.Id, Name = source.FirstName };
    private static EntityReference<long> ToDomain(Reservation source) =>
        new() { Id = source.Id, Name = string.Empty };
}