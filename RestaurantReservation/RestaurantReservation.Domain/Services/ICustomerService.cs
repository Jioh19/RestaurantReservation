using RestaurantReservation.Domain.Models.Customers;
using RestaurantReservation.Domain.Repository;

namespace RestaurantReservation.Domain.Services;

public interface ICustomerService
{
    Task<DomainCustomer> GetCustomerByIdAsync(long id);
    Task<IEnumerable<DomainCustomer>> GetAllCustomersAsync();
    Task<DomainCustomer> AddCustomerAsync(DomainCustomer domainCustomer);
    Task UpdateCustomerAsync(DomainCustomer domainCustomer);
    Task DeleteCustomerAsync(long id);
}