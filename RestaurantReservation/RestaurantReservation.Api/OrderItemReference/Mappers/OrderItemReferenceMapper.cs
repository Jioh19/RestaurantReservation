using RestaurantReservation.Api.Contracts.OrderItemReference;
using Riok.Mapperly.Abstractions;
using DomainOrderItemReference = RestaurantReservation.Domain.EntityReferences.OrderItemReference;

namespace RestaurantReservation.Api.OrderItemReference.Mappers;

[Mapper]
public static partial class OrderItemReferenceMapper
{
    public static partial OrderItemReferenceResponse ToResponse(this DomainOrderItemReference source);
    
    public static partial DomainOrderItemReference ToDomain(this OrderItemReferenceRequest source);
}