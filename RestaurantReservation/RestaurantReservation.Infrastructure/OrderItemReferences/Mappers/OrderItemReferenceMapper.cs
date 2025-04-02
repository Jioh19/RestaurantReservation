using RestaurantReservation.Domain.EntityReferences;
using Riok.Mapperly.Abstractions;
using DomainOrderItemReference = RestaurantReservation.Domain.EntityReferences.OrderItemReference;

namespace RestaurantReservation.Infrastructure.OrderItemReferences.Mappers;

[Mapper]
public static partial class OrderItemReferenceMapper
{
    public static partial DomainOrderItemReference ToDomain(this OrderItemReference source);
    
    public static partial OrderItemReference ToEntity(this DomainOrderItemReference source);
    
    public static partial void UpdateDomainToInfrastructure(this DomainOrderItemReference source, OrderItemReference domain);
}