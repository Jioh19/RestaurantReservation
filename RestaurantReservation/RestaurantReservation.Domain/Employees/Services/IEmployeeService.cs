using RestaurantReservation.Domain.Employees.Models;

namespace RestaurantReservation.Domain.Employees.Services;

public interface IEmployeeService
{
    Task<Employee> GetEmployeeByIdAsync(long id);
    Task<IReadOnlyCollection<Employee>> GetAllEmployeesAsync();
    Task<Employee> AddEmployeeAsync(Employee domainEmployee);
    Task UpdateEmployeeAsync(Employee domainEmployee);
    Task DeleteEmployeeAsync(long id);
    Task AddAllEmployeeAsync(IEnumerable<Employee> domainEmployees);
    Task<IReadOnlyCollection<Employee>> GetManagersAsync();
    Task<decimal> GetAverageOrderByEmployeeIdAsync(long id);
    Task<Employee?> ValidateCredentials(long id, string lastName);
}