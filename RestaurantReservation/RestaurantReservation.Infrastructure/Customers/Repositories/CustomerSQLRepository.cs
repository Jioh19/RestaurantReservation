using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Domain.Models.Customers;
using RestaurantReservation.Domain.Repository;
using RestaurantReservation.Infrastructure.Contexts;
using RestaurantReservation.Infrastructure.Mappers;

namespace RestaurantReservation.Infrastructure.Customers.Repositories;

public class CustomerSQLRepository : ICustomerRepository
{

    private readonly RestaurantReservationDbContext _context;
    private readonly ILogger<CustomerSQLRepository> _logger;

    public CustomerSQLRepository(RestaurantReservationDbContext context, ILogger<CustomerSQLRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<DomainCustomer>> GetAllAsync()
    {
        var customers = await _context.Customers.ToListAsync();
        _logger.LogInformation("Getting all customers" + " " + customers.Count);
        return customers.Select(c => c.ToDomain()).ToList();
    }
    
    public async Task<DomainCustomer?> GetByIdAsync(long id)
    {
        var customer =  await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
        return customer?.ToDomain();
    }

    public async Task<DomainCustomer> AddAsync(DomainCustomer domainCustomer)
    {
        await _context.Customers.AddAsync(domainCustomer.ToEntity());
        await _context.SaveChangesAsync();
        return domainCustomer;
    }

    public async Task<DomainCustomer> UpdateAsync(DomainCustomer domainCustomer)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == domainCustomer.CustomerId);
        if (customer is null) 
        {
            return null; // The customer with the given Id did not exist.
        }
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
        return domainCustomer;
    }

    public async Task DeleteAsync(long id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
        if (customer == null)
            return;
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }
}
