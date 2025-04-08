using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Contexts;
using RestaurantReservation.Infrastructure.Employees.Mappers;
using RestaurantReservation.Infrastructure.Employees.Models;
using DomainEmployee = RestaurantReservation.Domain.Employees.Models.Employee;

namespace RestaurantReservation.Infrastructure.Employees.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly RestaurantReservationDbContext _context;
    private readonly ILogger<EmployeeRepository> _logger;

    private IQueryable<Employee> FullQuery => _context.Employees.Include(e => e.Restaurant);

    public EmployeeRepository(RestaurantReservationDbContext context, ILogger<EmployeeRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<DomainEmployee>> GetAllAsync()
    {
        var employees = await FullQuery.ToListAsync();
        _logger.LogInformation("Getting all Employees" + " " + employees.Count);
        return employees.Select(t => t.ToDomain()).ToList();
    }

    public async Task<DomainEmployee?> GetByIdAsync(long id)
    {
        var employee = await FullQuery.FirstOrDefaultAsync(e => e.Id == id);
        return employee?.ToDomain();
    }

    public async Task<DomainEmployee> AddAsync(DomainEmployee domainEmployee)
    {
        var entity = domainEmployee.ToEntity();
        await _context.Employees.AddAsync(entity);
        await _context.SaveChangesAsync();
        return (await FullQuery
                .FirstAsync(e => e.Id == entity.Id))
            .ToDomain();
    }

    public async Task<DomainEmployee?> UpdateAsync(DomainEmployee domainEmployee)
    {
        var employee = await FullQuery.FirstOrDefaultAsync(e => e.Id == domainEmployee.Id);
        if (employee is null)
        {
            _logger.LogError("Employee not found");
            return null;
        }

        var restaurant = await _context.Restaurants.FindAsync(domainEmployee.Restaurant.Id);
        if (restaurant is null)
        {
            _logger.LogError("Restaurant not found");
            return null;
        }

        employee.Restaurant = restaurant;
        _logger.LogInformation("Updating Employee" + " " + employee.Restaurant.Name);
        EmployeeMapper.UpdateDomainToInfrastructure(domainEmployee, employee);
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        return employee.ToDomain();
    }

    public async Task DeleteAsync(long id)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(t => t.Id == id);
        if (employee is null)
        {
            return;
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }

    public async Task AddAllAsync(IEnumerable<DomainEmployee> domainEmployees)
    {
        await _context.Employees.AddRangeAsync(domainEmployees.Select(t => t.ToEntity()).ToList());
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<DomainEmployee>> GetManagersAsync()
    {
        var employees = await _context.Employees.Where(t => t.Position.Equals("Manager")).ToListAsync();
        _logger.LogInformation("Getting all Managers" + " " + employees.Count);
        return employees.Select(t => t.ToDomain()).ToList();
    }

    public async ValueTask<decimal> GetAverageOrderByEmployeeIdAsync(long id)
    {
        var average = await _context.Orders.Where(o => o.EmployeeId == id).AverageAsync(o => o.TotalAmount);
        return average;
    }
}