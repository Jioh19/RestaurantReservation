using RestaurantReservation.Domain.Models.Customers;
using RestaurantReservation.Domain.Repository;

namespace RestaurantReservation.Domain.Services;

public class CustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer> GetCustomerByIdAsync(long id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        return customer;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers;
    }

    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        var response = await _customerRepository.AddAsync(customer);
        return response;
    }
}