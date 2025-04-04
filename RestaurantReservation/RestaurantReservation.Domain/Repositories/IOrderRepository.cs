using RestaurantReservation.Domain.EntityReferences;
using RestaurantReservation.Domain.Orders.Models;

namespace RestaurantReservation.Domain.Repositories;

public interface IOrderRepository: IRepository<Order>
{
    Task AddAllAsync(IEnumerable<Order> domainOrders);
    Task<IReadOnlyCollection<Order>> GetOrdersByReservationIdAsync(long reservationId);
    Task<IReadOnlyCollection<OrderItemReference>> GetOrderItemsByOrderIdAsync(long orderId);
}