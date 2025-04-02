using RestaurantReservation.Infrastructure.OrderItemReferences.Models;
using Riok.Mapperly.Abstractions;
using DomainOrderItemReference = RestaurantReservation.Domain.EntityReferences.OrderItemReference;

namespace RestaurantReservation.Infrastructure.OrderItemReferences.Mappers;

[Mapper]
public static partial class OrderItemReferenceMapper
{
    [MapProperty(nameof(OrderItemReference.MenuItem.Name), nameof(DomainOrderItemReference.Name))]
    [MapperIgnoreSource(nameof(OrderItemReference.Order))]
    [MapperIgnoreSource(nameof(OrderItemReference.OrderId))]
    [MapperIgnoreSource(nameof(OrderItemReference.MenuItemId))]
    public static partial DomainOrderItemReference ToDomain(this OrderItemReference source);
    
    [MapProperty(nameof(DomainOrderItemReference.Name), nameof(OrderItemReference.MenuItem.Name))]
    [MapperIgnoreTarget(nameof(OrderItemReference.Order))]
    [MapperIgnoreTarget(nameof(OrderItemReference.OrderId))]
    [MapperIgnoreTarget(nameof(OrderItemReference.MenuItemId))]
    public static partial OrderItemReference ToEntity(this DomainOrderItemReference source);
    
    [MapProperty(nameof(DomainOrderItemReference.Name), nameof(OrderItemReference.MenuItem.Name))]
    [MapperIgnoreTarget(nameof(OrderItemReference.Order))]
    [MapperIgnoreTarget(nameof(OrderItemReference.OrderId))]
    [MapperIgnoreTarget(nameof(OrderItemReference.MenuItemId))]
    public static partial void UpdateDomainToInfrastructure(this DomainOrderItemReference source, OrderItemReference domain);
}