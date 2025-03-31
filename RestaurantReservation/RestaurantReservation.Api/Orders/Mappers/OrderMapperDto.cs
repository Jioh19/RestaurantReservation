using RestaurantReservation.Api.Contracts.Orders.Models;
using Riok.Mapperly.Abstractions;
using DomainOrder = RestaurantReservation.Domain.Orders.Models.Order;

namespace RestaurantReservation.Api.Orders.Mappers;

[Mapper]
public static partial class OrderMapperDto
{
    public static partial OrderResponse ToResponse(this DomainOrder source);
    
    public static partial DomainOrder ToDomain(this OrderRequest source);
}