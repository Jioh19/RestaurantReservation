using RestaurantReservation.Domain.Errors;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Domain.EntityReferences.Service;

public class OrderItemReferenceService : IOrderItemReferenceService
{
    private readonly IOrderItemReferenceRepository _orderItemReferenceRepository;

    public OrderItemReferenceService(IOrderItemReferenceRepository orderItemReferenceRepository)
    {
        _orderItemReferenceRepository = orderItemReferenceRepository;
    }

    public async Task<OrderItemReference> GetOrderItemReferenceByIdAsync(long id)
    {
        var orderItemReference = await _orderItemReferenceRepository.GetByIdAsync(id);
        if (orderItemReference is null)
        {
            throw new EntityNotFoundException<OrderItemReference>(id.ToString());
        }

        return orderItemReference;
    }

    public async Task<IReadOnlyCollection<OrderItemReference>> GetAllOrderItemReferencesAsync()
    {
        var orderItemReferences = await _orderItemReferenceRepository.GetAllAsync();
        return orderItemReferences.ToList();
    }

    public async Task<OrderItemReference> AddOrderItemReferenceAsync(OrderItemReference domainOrderItemReference)
    {
        var orderItemReference = await _orderItemReferenceRepository.AddAsync(domainOrderItemReference);
        return orderItemReference;
    }

    public async Task UpdateOrderItemReferenceAsync(OrderItemReference domainOrderItemReference)
    {
        await _orderItemReferenceRepository.UpdateAsync(domainOrderItemReference);
    }

    public async Task DeleteOrderItemReferenceAsync(long id)
    {
        await _orderItemReferenceRepository.DeleteAsync(id);
    }
}