using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Domain.Models.Customers;
using RestaurantReservation.Domain.Repository;
using RestaurantReservation.Infrastructure.Contexts;

namespace RestaurantReservation.Infrastructure.Customers.Repositories;

public class CustomerSQLRepository : ICustomerRepository
{

    private readonly RestaurantReservationDbContext _context;

    public CustomerSQLRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Set<Customer>().ToListAsync();
    }

    //Poner atencion a los nullable ?
    public async Task<Customer?> GetByIdAsync(long id)
    {
        return await _context.Set<Customer>().FirstOrDefaultAsync(c => c.CustomerId == id);
    }

    public async Task<Customer> AddAsync(Customer customer)
    {
        await _context.Set<Customer>().AddAsync(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> UpdateAsync(Customer customer)
    {
        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task DeleteAsync(long id)
    {
        Customer? customer = await GetByIdAsync(id);
        if (customer == null)
            return;
        _context.Set<Customer>().Remove(customer);
        await _context.SaveChangesAsync();
    }
}