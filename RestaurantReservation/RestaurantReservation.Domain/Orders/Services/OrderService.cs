﻿using RestaurantReservation.Domain.EntityReferences;
using RestaurantReservation.Domain.Errors;
using RestaurantReservation.Domain.Orders.Models;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Domain.Orders.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order> GetOrderByIdAsync(long id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order is null)
        {
            throw new EntityNotFoundException<Order>(id.ToString());
        }

        return order;
    }

    public async Task<IReadOnlyCollection<Order>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.ToList();
    }

    public async Task<Order> AddOrderAsync(Order domainOrder)
    {
        Console.WriteLine($"Creando order en service {domainOrder.Employee.Id}");
        var order = await _orderRepository.AddAsync(domainOrder);
        return order;
    }

    public async Task UpdateOrderAsync(Order domainOrder)
    {
        await _orderRepository.UpdateAsync(domainOrder);
    }

    public async Task DeleteOrderAsync(long id)
    {
        await _orderRepository.DeleteAsync(id);
    }
    
    public async Task AddAllOrderAsync(IEnumerable<Order> domainOrders)
    {
        await _orderRepository.AddAllAsync(domainOrders);
    }
    
    public async Task<IReadOnlyCollection<Order>> GetOrdersByReservationIdAsync(long reservationId)
    {
        var orders = await _orderRepository.GetOrdersByReservationIdAsync(reservationId);
        return orders.ToList();
    }
    
    public Task<IReadOnlyCollection<OrderItemReference>> GetOrderItemsByOrderIdAsync(long orderId) =>
        _orderRepository.GetOrderItemsByOrderIdAsync(orderId);
}