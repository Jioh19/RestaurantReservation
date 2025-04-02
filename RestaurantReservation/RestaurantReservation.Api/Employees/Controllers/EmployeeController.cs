using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Api.Contracts.Employees.Models;
using RestaurantReservation.Api.Employees.Mappers;
using RestaurantReservation.Domain.Employees.Services;
using RestaurantReservation.Domain.Errors;
using RestaurantReservation.Domain.Restaurants.Services;
using DomainEmployee = RestaurantReservation.Domain.Employees.Models.Employee;

namespace RestaurantReservation.Api.Employees.Controllers;


[ApiController]
[Route("api/employee")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IRestaurantService _restaurantService;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IEmployeeService employeeService, IRestaurantService restaurantService, ILogger<EmployeeController> logger)
    {
        _employeeService = employeeService;
        _restaurantService = restaurantService;
        _logger = logger;
    }

    // New GetEmployee method
    [HttpGet("{id}", Name = "GetEmployee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeResponse>> GetEmployee(long id)
    {
        try
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(employee.ToResponse());
        }
        catch (EntityNotFoundException<DomainEmployee>)
        {
            _logger.LogError($"Employee with id {id} not found");
            return NotFound($"Employee with id {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving employee with id {id}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the employee");
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetEmployees()
    {
        try
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees.Select(EmployeeMapperDto.ToResponse).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving employees");
            return StatusCode(500, "An error occurred while retrieving employees");
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EmployeeResponse>> CreateEmployee([FromBody] EmployeeRequest employeeRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _logger.Log(LogLevel.Information, $"Creating employee");
        try
        {
            var createdEmployee = await _employeeService.AddEmployeeAsync(employeeRequest.ToDomain());
            var response = createdEmployee.ToResponse();
            return CreatedAtRoute("GetEmployee", new { id = response.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating employee");
            return StatusCode(500, "An error occurred while creating the employee");
        }
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateEmployee(long id, [FromBody] EmployeeRequest employeeRequest)
    {
        try
        {
            _logger.Log(LogLevel.Information, $"Updating employee");
            var employee = employeeRequest.ToDomain();
            employee.Id = id;
            await _employeeService.UpdateEmployeeAsync(employee);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Employee with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating employee with ID {id}");
            return StatusCode(500, "An error occurred while updating the employee");
        }
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteEmployee(long id)
    {
        try
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }
        catch (EntityNotFoundException<DomainEmployee>)
        {
            return NotFound($"Employee with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting employee with ID {id}");
            return StatusCode(500, "An error occurred while deleting the employee");
        }
    }
    
    [HttpPost("import")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateBatchEmployees([FromBody] IEnumerable<EmployeeRequest> employeeRequests)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid employee data");
        }
        _logger.Log(LogLevel.Information, $"Creating many employees");
        try
        {
            await _employeeService.AddAllEmployeeAsync(employeeRequests.Select(e => e.ToDomain()).ToList());
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating employees");
            return BadRequest("Error creating employees");
        }
    }
    
    [HttpGet("managers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetManagers()
    {
        try
        {
            var employees = await _employeeService.GetManagersAsync();
            return Ok(employees.Select(EmployeeMapperDto.ToResponse).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving managers");
            return StatusCode(500, "An error occurred while retrieving managers");
        }
    }
    
    [HttpGet("{id}/average-order-amount")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetAverageOrderByEmployeeId(long id)
    {
        try
        {
            var average = await _employeeService.GetAverageOrderByEmployeeIdAsync(id);
            return Ok(average);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating average");
            return StatusCode(500, "An error occurred while calculating average");
        }
    }
}