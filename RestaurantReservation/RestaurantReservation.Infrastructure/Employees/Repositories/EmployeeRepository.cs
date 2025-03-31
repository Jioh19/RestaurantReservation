using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Contexts;
using RestaurantReservation.Infrastructure.Employees.Mappers;
using RestaurantReservation.Infrastructure.Restaurants.Models;
using DomainEmployee = RestaurantReservation.Domain.Employees.Models.Employee;

namespace RestaurantReservation.Infrastructure.Employees.Repositories;

public class EmployeeRepository :  IEmployeeRepository
{
    private readonly RestaurantReservationDbContext _context;
    private readonly ILogger<EmployeeRepository> _logger;

    public EmployeeRepository(RestaurantReservationDbContext context, ILogger<EmployeeRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<IEnumerable<DomainEmployee>> GetAllAsync()
    {
        var employees = await _context.Employees.ToListAsync();
        _logger.LogInformation("Getting all Employees" + " " + employees.Count);
        return employees.Select(t => t.ToDomain()).ToList();
    }

    public async Task<DomainEmployee?> GetByIdAsync(long id)
    {
        var employee = await _context.Employees.FindAsync(id);
        return employee?.ToDomain();
    }

    public async Task<DomainEmployee> AddAsync(DomainEmployee domainEmployee)
    {
        var response = await _context.Employees.AddAsync(domainEmployee.ToEntity());
        await _context.SaveChangesAsync();
        return response.Entity.ToDomain();
    }

    public async Task<DomainEmployee?> UpdateAsync(DomainEmployee domainEmployee)
    {
        var employee = await _context.Employees.FindAsync(domainEmployee.Id);
        if (employee is null)
        {
            _logger.LogError("Employee not found");
            return null;
        }
        
        var restaurant =  await _context.Restaurants.FindAsync(domainEmployee.RestaurantId);
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
}