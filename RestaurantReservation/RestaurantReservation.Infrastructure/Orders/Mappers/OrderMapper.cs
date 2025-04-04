using RestaurantReservation.Infrastructure.Orders.Models;
using Riok.Mapperly.Abstractions;
using DomainOrder = RestaurantReservation.Domain.Orders.Models.Order;

namespace RestaurantReservation.Infrastructure.Orders.Mappers;

[Mapper]
public static partial class OrderMapper
{

    [MapperIgnoreSource(nameof(Order.Employee))]
    [MapperIgnoreSource(nameof(Order.OrderItems))]
    [MapperIgnoreSource(nameof(Order.Reservation))]
    [MapperIgnoreTarget(nameof(DomainOrder.Items))]
    public static partial DomainOrder ToDomain(this Order source);
    
    [MapperIgnoreTarget(nameof(Order.Employee))]
    [MapperIgnoreTarget(nameof(Order.OrderItems))]
    [MapperIgnoreTarget(nameof(Order.Reservation))]
    [MapperIgnoreSource(nameof(DomainOrder.Items))]
    public static partial Order ToEntity(this DomainOrder source);
    
    [MapperIgnoreTarget(nameof(Order.Employee))]
    [MapperIgnoreTarget(nameof(Order.OrderItems))]
    [MapperIgnoreTarget(nameof(Order.Reservation))]
    [MapperIgnoreSource(nameof(DomainOrder.Items))]
    public static partial void UpdateDomainToInfrastructure(this DomainOrder source, Order domain);
}