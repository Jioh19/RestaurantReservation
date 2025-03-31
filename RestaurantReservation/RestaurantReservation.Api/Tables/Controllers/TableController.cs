using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Api.Contracts.Tables.Models;
using RestaurantReservation.Api.Tables.Mappers;
using RestaurantReservation.Domain.Errors;
using RestaurantReservation.Domain.Restaurants.Services;
using RestaurantReservation.Domain.Tables.Services;
using DomainTable = RestaurantReservation.Domain.Tables.Models.Table;

namespace RestaurantReservation.Api.Tables.Controllers;

[ApiController]
[Route("api/table")]
public class TableController : ControllerBase
{
    private readonly ITableService _tableService;
    private readonly IRestaurantService _restaurantService;
    private readonly ILogger<TableController> _logger;

    public TableController(ITableService tableService, IRestaurantService restaurantService, ILogger<TableController> logger)
    {
        _tableService = tableService;
        _restaurantService = restaurantService;
        _logger = logger;
    }

    // New GetTable method
    [HttpGet("{id}", Name = "GetTable")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TableResponse>> GetTable(long id)
    {
        try
        {
            var table = await _tableService.GetTableByIdAsync(id);
            return Ok(table.ToResponse());
        }
        catch (EntityNotFoundException<DomainTable>)
        {
            _logger.LogError($"Table with id {id} not found");
            return NotFound($"Table with id {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving table with id {id}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the table");
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TableResponse>>> GetTables()
    {
        try
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables.Select(TableMapperDto.ToResponse).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving tables");
            return StatusCode(500, "An error occurred while retrieving tables");
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TableResponse>> CreateTable([FromBody] TableRequest tableRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _logger.Log(LogLevel.Information, $"Creating table");
        try
        {
            var createdTable = await _tableService.AddTableAsync(tableRequest.ToDomain());
            var response = createdTable.ToResponse();
            return CreatedAtRoute("GetTable", new { id = response.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating table");
            return StatusCode(500, "An error occurred while creating the table");
        }
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateTable(long id, [FromBody] TableRequest tableRequest)
    {
        try
        {
            _logger.Log(LogLevel.Information, $"Updating table");
            tableRequest.Id = id;
            await _tableService.UpdateTableAsync(tableRequest.ToDomain());
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Table with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating table with ID {id}");
            return StatusCode(500, "An error occurred while updating the table");
        }
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteTable(long id)
    {
        try
        {
            await _tableService.DeleteTableAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Table with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting table with ID {id}");
            return StatusCode(500, "An error occurred while deleting the table");
        }
    }
}