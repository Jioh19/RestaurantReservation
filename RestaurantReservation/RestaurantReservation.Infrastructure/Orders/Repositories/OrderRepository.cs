using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Contexts;
using RestaurantReservation.Infrastructure.Orders.Mappers;
using DomainOrder = RestaurantReservation.Domain.Orders.Models.Order;

namespace RestaurantReservation.Infrastructure.Orders.Repositories;


public class OrderRepository : IOrderRepository
{
    private readonly RestaurantReservationDbContext _context;
    private readonly ILogger<OrderRepository> _logger;

    public OrderRepository(RestaurantReservationDbContext context, ILogger<OrderRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<IEnumerable<DomainOrder>> GetAllAsync()
    {
        var order = await _context.Orders.ToListAsync();
        _logger.LogInformation("Getting all Orders" + " " + order.Count);
        return order.Select(t => t.ToDomain()).ToList();
    }

    public async Task<DomainOrder?> GetByIdAsync(long id)
    {
        var table = await _context.Orders.FindAsync(id);
        return table?.ToDomain();
    }

    public async Task<DomainOrder> AddAsync(DomainOrder domainOrder)
    {
        var response = await _context.Orders.AddAsync(domainOrder.ToEntity());
        await _context.SaveChangesAsync();
        return response.Entity.ToDomain();
    }

    public async Task<DomainOrder?> UpdateAsync(DomainOrder domainOrder)
    {
        var table = await _context.Orders.FindAsync(domainOrder.Id);
        if (table is null)
        {
            _logger.LogError("Order not found");
            return null;
        }
        OrderMapper.UpdateDomainToInfrastructure(domainOrder, table);
        _context.Orders.Update(table);
        await _context.SaveChangesAsync();
        return table.ToDomain();
    }

    public async Task DeleteAsync(long id)
    {
        var table = await _context.Orders.FirstOrDefaultAsync(t => t.Id == id);
        if (table is null)
        {
            return;
        }
        _context.Orders.Remove(table);
        await _context.SaveChangesAsync();
    }
}