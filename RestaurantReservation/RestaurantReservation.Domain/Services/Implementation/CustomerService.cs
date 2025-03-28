using RestaurantReservation.Domain.Models.Customers;
using RestaurantReservation.Domain.Repository;
using RestaurantReservation.Domain.Services;


public class CustomerService :  ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<DomainCustomer> GetCustomerByIdAsync(long id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        return customer;
    }

    public async Task<IEnumerable<DomainCustomer>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers;
    }

    public async Task<DomainCustomer> AddCustomerAsync(DomainCustomer domainCustomer)
    {
        var response = await _customerRepository.AddAsync(domainCustomer);
        return response;
    }
    
    public async Task UpdateCustomerAsync(DomainCustomer domainCustomer)
    {
        await _customerRepository.UpdateAsync(domainCustomer);
    }
    
    public async Task DeleteCustomerAsync(long id)
    {
        await _customerRepository.DeleteAsync(id);
    }
}