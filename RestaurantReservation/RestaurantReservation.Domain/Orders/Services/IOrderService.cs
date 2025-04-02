using RestaurantReservation.Domain.Orders.Models;

namespace RestaurantReservation.Domain.Orders.Services;

public interface IOrderService
{
    Task<Order> GetOrderByIdAsync(long id);
    Task<IReadOnlyCollection<Order>> GetAllOrdersAsync();
    Task<Order> AddOrderAsync(Order domainOrder);
    Task UpdateOrderAsync(Order domainOrder);
    Task DeleteOrderAsync(long id);
    Task AddAllOrderAsync(IEnumerable<Order> domainOrders);
    Task<IReadOnlyCollection<Order?>> GetOrdersByReservationIdAsync(long reservationId);
}