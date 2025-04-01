using RestaurantReservation.Infrastructure.Orders.Models;
using Riok.Mapperly.Abstractions;
using DomainOrder = RestaurantReservation.Domain.Orders.Models.Order;

namespace RestaurantReservation.Infrastructure.Orders.Mappers;

[Mapper]
public static partial class OrderMapper
{

    public static partial DomainOrder ToDomain(this Order source);
    
    public static partial Order ToEntity(this DomainOrder source);
    
    public static partial void UpdateDomainToInfrastructure(this DomainOrder source, Order domain);
}