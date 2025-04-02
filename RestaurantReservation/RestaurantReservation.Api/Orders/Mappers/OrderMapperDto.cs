using RestaurantReservation.Api.Contracts.EntityReferences;
using RestaurantReservation.Api.Contracts.Orders.Models;
using RestaurantReservation.Domain.EntityReferences;
using Riok.Mapperly.Abstractions;
using DomainOrder = RestaurantReservation.Domain.Orders.Models.Order;

namespace RestaurantReservation.Api.Orders.Mappers;

[Mapper]
public static partial class OrderMapperDto
{
    [MapperIgnoreSource(nameof(DomainOrder.Items))]
    public static partial OrderResponse ToResponse(this DomainOrder source);
    
    [MapperIgnoreTarget(nameof(DomainOrder.Items))]
    public static partial DomainOrder ToDomain(this OrderRequest source);

    public static partial OrderItemReferenceResponse ToResponse(this OrderItemReference source);
}