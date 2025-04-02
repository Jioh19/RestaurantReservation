namespace RestaurantReservation.Domain.EntityReferences.Service;

public interface IOrderItemReferenceService
{
    Task<OrderItemReference> GetOrderItemReferenceByIdAsync(long id);
    Task<IReadOnlyCollection<OrderItemReference>> GetAllOrderItemReferencesAsync();
    Task<OrderItemReference> AddOrderItemReferenceAsync(OrderItemReference domainOrderItemReference);
    Task UpdateOrderItemReferenceAsync(OrderItemReference domainOrderItemReference);
    Task DeleteOrderItemReferenceAsync(long id);
}