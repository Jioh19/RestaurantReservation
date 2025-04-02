using RestaurantReservation.Domain.Employees.Models;

namespace RestaurantReservation.Domain.Repositories;

public interface IEmployeeRepository: IRepository<Employee>
{
    Task AddAllAsync(IEnumerable<Employee> domainEmployees);
    Task<IReadOnlyCollection<Employee>> GetManagersAsync();
    ValueTask<decimal> GetAverageOrderByEmployeeIdAsync(long id);
}