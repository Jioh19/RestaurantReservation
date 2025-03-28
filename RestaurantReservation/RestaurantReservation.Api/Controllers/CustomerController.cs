using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Domain.Models.Customers;
using RestaurantReservation.Domain.Repository;
using RestaurantReservation.Domain.Services;

namespace RestaurantReservation.Api.Controllers;

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
    public async Task<ActionResult<DomainCustomer>> GetCustomer(int id)
    {
        try
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer is null)
            {
                _logger.LogWarning($"Customer with id {id} not found");
                return NotFound($"Customer with id {id} not found");
            }

            return Ok(customer);
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
    public async Task<ActionResult<IEnumerable<DomainCustomer>>> GetCustomers()
    {
        try
        {
            var customers = await _customerService.GetAllCustomersAsync();
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
    public async Task<ActionResult<DomainCustomer>> CreateCustomer([FromBody] DomainCustomer domainCustomer)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _logger.Log(LogLevel.Information, "Creating customer" +" "+ domainCustomer.FirstName + "  " + domainCustomer.CustomerId);
        try
        {
            var createdCustomer = await _customerService.AddCustomerAsync(domainCustomer);
            return CreatedAtRoute("GetCustomer", new { id = createdCustomer.CustomerId }, createdCustomer);
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
    public async Task<IActionResult> UpdateCustomer(long id, [FromBody] DomainCustomer domainCustomer)
    {
        domainCustomer.CustomerId = id;
        try
        {
            await _customerService.UpdateCustomerAsync(domainCustomer);
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