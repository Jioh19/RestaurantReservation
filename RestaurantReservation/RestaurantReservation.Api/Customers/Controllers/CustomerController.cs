using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Api.Contracts.Customers.Models;
using RestaurantReservation.Api.Customers.Mappers;
using DomainCustomer = RestaurantReservation.Domain.Customers.Models.Customer;
using RestaurantReservation.Domain.Customers.Services;
using RestaurantReservation.Domain.Errors;

namespace RestaurantReservation.Api.Customers.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
    {
        _customerService = customerService;
        _logger = logger;
    }
    
    // New GetCustomer method
    [HttpGet("{id}", Name = "GetCustomer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerResponse>> GetCustomer(long id)
    {
        try
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            return Ok(customer.ToResponse());
        }
        catch (EntityNotFoundException<DomainCustomer>)
        {
            _logger.LogError($"Customer with id {id} not found");
            return NotFound($"Customer with id {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving customer with id {id}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the customer");
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetCustomers()
    {
        try
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers.Select(CustomerMapperDto.ToResponse).ToList());
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
    public async Task<ActionResult<CustomerResponse>> CreateCustomer([FromBody] CustomerRequest customerRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _logger.Log(LogLevel.Information, $"Creating customer {customerRequest.FirstName}");
        try
        {
            var createdCustomer = await _customerService.AddCustomerAsync(customerRequest.ToDomain());
            return CreatedAtRoute("GetCustomer", new { id = createdCustomer.Id }, createdCustomer.ToResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating customer");
            return StatusCode(500, "An error occurred while creating the customer");
        }
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCustomer(long id, [FromBody] CustomerRequest customerRequest)
    {
        try
        {
            customerRequest.Id = id;
            await _customerService.UpdateCustomerAsync(customerRequest.ToDomain());
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Customer with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating customer with ID {id}");
            return StatusCode(500, "An error occurred while updating the customer");
        }
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCustomer(long id)
    {
        try
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Customer with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting customer with ID {id}");
            return StatusCode(500, "An error occurred while deleting the customer");
        }
    }
}