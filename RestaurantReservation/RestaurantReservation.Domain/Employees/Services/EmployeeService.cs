using RestaurantReservation.Domain.Employees.Models;
using RestaurantReservation.Domain.Errors;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Domain.Employees.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Employee> GetEmployeeByIdAsync(long id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee is null)
        {
            throw new EntityNotFoundException<Employee>(id.ToString());
        }

        return employee;
    }

    public async Task<IReadOnlyCollection<Employee>> GetAllEmployeesAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return employees.ToList();
    }

    public async Task<Employee> AddEmployeeAsync(Employee domainEmployee)
    {
        var employee = await _employeeRepository.AddAsync(domainEmployee);
        return employee;
    }

    public async Task UpdateEmployeeAsync(Employee domainEmployee)
    {
        await _employeeRepository.UpdateAsync(domainEmployee);
    }

    public async Task DeleteEmployeeAsync(long id)
    {
        await _employeeRepository.DeleteAsync(id);
    }

    public async Task AddAllEmployeeAsync(IEnumerable<Employee> domainEmployees)
    {
        await _employeeRepository.AddAllAsync(domainEmployees);
    }

    public async Task<IReadOnlyCollection<Employee>> GetManagersAsync()
    {
        var employees = await _employeeRepository.GetManagersAsync();
        return employees.ToList();
    }

    public async Task<decimal> GetAverageOrderByEmployeeIdAsync(long id)
    {
        var average = await _employeeRepository.GetAverageOrderByEmployeeIdAsync(id);
        return average;
    }
    
    public async Task<Employee?> ValidateCredentials(long id, string lastName)
    {
        try
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return null;
            }
            bool isValidLastName = employee.LastName.Equals(lastName);
            
            if (!isValidLastName)
            {
                return null;
            }

            return employee;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}