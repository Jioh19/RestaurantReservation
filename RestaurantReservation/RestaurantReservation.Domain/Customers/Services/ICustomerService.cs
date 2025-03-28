using RestaurantReservation.Domain.Customers.Models;
using RestaurantReservation.Domain.Repository;

namespace RestaurantReservation.Domain.Customers.Services;

public interface ICustomerService
{
    Task<Customer> GetCustomerByIdAsync(long id);
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> AddCustomerAsync(Customer domainCustomer);
    Task UpdateCustomerAsync(Customer domainCustomer);
    Task DeleteCustomerAsync(long id);
}