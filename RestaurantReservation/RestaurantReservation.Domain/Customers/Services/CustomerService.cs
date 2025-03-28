using RestaurantReservation.Domain.Customers.Models;
using RestaurantReservation.Domain.Repository;
using RestaurantReservation.Domain.Customers.Services;
using RestaurantReservation.Domain.Errors;


public class CustomerService :  ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer> GetCustomerByIdAsync(long id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer is null)
        {
            throw new EntityNotFoundException<Customer>(id.ToString());
        }
        return customer;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers;
    }

    public async Task<Customer> AddCustomerAsync(Customer domainCustomer)
    {
        var response = await _customerRepository.AddAsync(domainCustomer);
        return response;
    }
    
    public async Task UpdateCustomerAsync(Customer domainCustomer)
    {
        await _customerRepository.UpdateAsync(domainCustomer);
    }
    
    public async Task DeleteCustomerAsync(long id)
    {
        await _customerRepository.DeleteAsync(id);
    }
}