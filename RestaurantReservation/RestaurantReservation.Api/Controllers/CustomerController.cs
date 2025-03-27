using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Domain.Models.Customers;
using RestaurantReservation.Domain.Repository;

namespace RestaurantReservation.Api.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ICustomerRepository customerRepository, ILogger<CustomerController> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        try
        {
            var customers = await _customerRepository.GetAllAsync();
            return Ok(customers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving customers");
            return StatusCode(500, "An error occurred while retrieving customers");
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Customer>> CreateCustomer([FromBody] Customer customer)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _logger.Log(LogLevel.Information, "Creating customer" +" "+ customer.FirstName);
        try
        {
            var createdCustomer = await _customerRepository.AddAsync(customer);
            return CreatedAtRoute("GetCustomer", new { id = createdCustomer.CustomerId }, createdCustomer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating customer");
            return StatusCode(500, "An error occurred while creating the customer");
        }
    }
}