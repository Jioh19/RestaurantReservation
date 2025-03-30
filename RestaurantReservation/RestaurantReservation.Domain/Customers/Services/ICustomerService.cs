using RestaurantReservation.Domain.Customers.Models;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Domain.Customers.Services;

public interface ICustomerService
{
    Task<Customer> GetCustomerByIdAsync(long id);
    Task<IReadOnlyCollection<Customer>> GetAllCustomersAsync();
    Task<Customer> AddCustomerAsync(Customer domainCustomer);
    Task UpdateCustomerAsync(Customer domainCustomer);
    Task DeleteCustomerAsync(long id);
}