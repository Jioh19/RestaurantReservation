using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Domain.EntityReferences;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Contexts;
using RestaurantReservation.Infrastructure.Orders.Mappers;
using RestaurantReservation.Infrastructure.Orders.Models;
using DomainOrder = RestaurantReservation.Domain.Orders.Models.Order;
using DomainOrderItemReference = RestaurantReservation.Domain.EntityReferences.OrderItemReference;

namespace RestaurantReservation.Infrastructure.Orders.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly RestaurantReservationDbContext _context;
    private readonly ILogger<OrderRepository> _logger;
    
    private IQueryable<Order> FullQuery => _context.Orders
        .Include(r => r.Employee)
        .Include( r => r.Reservation);

    public OrderRepository(RestaurantReservationDbContext context, ILogger<OrderRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<IReadOnlyCollection<DomainOrder>> GetAllAsync()
    {
        var order = await FullQuery.ToListAsync();
        _logger.LogInformation("Getting all Orders" + " " + order.Count);
        return order.Select(t => t.ToDomain()).ToList();
    }

    public async Task<DomainOrder?> GetByIdAsync(long id)
    {
        var table = await FullQuery.FirstOrDefaultAsync(r => r.Id == id);
        return table?.ToDomain();
    }

    public async Task<DomainOrder> AddAsync(DomainOrder domainOrder)
    {
        var entity = domainOrder.ToEntity();
        _logger.LogInformation($"Adding Order {entity.TotalAmount} {entity.ReservationId} {entity.EmployeeId}");
        await _context.Orders.AddAsync(entity);
        await _context.SaveChangesAsync();
        return (await FullQuery
                .FirstAsync(o => o.Id == entity.Id))
            .ToDomain();
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
    
    public async Task AddAllAsync(IEnumerable<DomainOrder> domainOrders)
    {
        await _context.Orders.AddRangeAsync(domainOrders.Select(t => t.ToEntity()).ToList());
        await _context.SaveChangesAsync();
    }
    
    public async Task<IReadOnlyCollection<DomainOrder>> GetOrdersByReservationIdAsync(long reservationId)
    {
        var orders = await _context.Orders.Where(t => t.ReservationId == reservationId).ToListAsync();
        _logger.LogInformation($"Getting all Orders by Reservation Id {reservationId}");
        return orders.Select(t => t.ToDomain()).ToList();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<DomainOrderItemReference>> GetOrderItemsByOrderIdAsync(long orderId) => 
        await _context.MenuItems
        .Join(
            _context.OrderItemReferences,
            mi => new { MenuItemId = mi.Id, OrderId = orderId },
            oir => new { oir.MenuItemId, oir.OrderId },
            (mi, oir) => oir
        )
        .Select(oir => DomainOrderItemReference.Create(oir.Id, oir.MenuItem.Name, oir.Quantity))
        .ToListAsync();
}