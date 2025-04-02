using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Contexts;
using RestaurantReservation.Infrastructure.OrderItemReferences.Mappers;
using DomainOrderItemReference = RestaurantReservation.Domain.EntityReferences.OrderItemReference;

namespace RestaurantReservation.Infrastructure.OrderItemReferences.Repositories;

public class OrderItemReferenceRepository :  IOrderItemReferenceRepository
{
     private readonly RestaurantReservationDbContext _context;
    private readonly ILogger<OrderItemReferenceRepository> _logger;

    public OrderItemReferenceRepository(RestaurantReservationDbContext context, ILogger<OrderItemReferenceRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<IReadOnlyCollection<DomainOrderItemReference>> GetAllAsync()
    {
        var employees = await _context.OrderItemReferences.ToListAsync();
        _logger.LogInformation("Getting all OrderItemReferences" + " " + employees.Count);
        return employees.Select(t => t.ToDomain()).ToList();
    }

    public async Task<DomainOrderItemReference?> GetByIdAsync(long id)
    {
        var employee = await _context.OrderItemReferences.FindAsync(id);
        return employee?.ToDomain();
    }

    public async Task<DomainOrderItemReference> AddAsync(DomainOrderItemReference domainOrderItemReference)
    {
        var response = await _context.OrderItemReferences.AddAsync(domainOrderItemReference.ToEntity());
        await _context.SaveChangesAsync();
        return response.Entity.ToDomain();
    }

    public async Task<DomainOrderItemReference?> UpdateAsync(DomainOrderItemReference domainOrderItemReference)
    {
        var employee = await _context.OrderItemReferences.FindAsync(domainOrderItemReference.Id);
        if (employee is null)
        {
            _logger.LogError("OrderItemReference not found");
            return null;
        }
        
        OrderItemReferenceMapper.UpdateDomainToInfrastructure(domainOrderItemReference, employee);
        _context.OrderItemReferences.Update(employee);
        await _context.SaveChangesAsync();
        return employee.ToDomain();
    }

    public async Task DeleteAsync(long id)
    {
        var employee = await _context.OrderItemReferences.FirstOrDefaultAsync(t => t.Id == id);
        if (employee is null)
        {
            return;
        }
        _context.OrderItemReferences.Remove(employee);
        await _context.SaveChangesAsync();
    }
    
    public async Task AddAllAsync(IEnumerable<DomainOrderItemReference> domainOrderItemReferences)
    {
        await _context.OrderItemReferences.AddRangeAsync(domainOrderItemReferences.Select(t => t.ToEntity()).ToList());
        await _context.SaveChangesAsync();
    }

}