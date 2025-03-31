using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Api.Contracts.MenuItem;
using RestaurantReservation.Api.MenuItem.Mappers;
using RestaurantReservation.Domain.Errors;
using RestaurantReservation.Domain.MenuItems.Service;
using RestaurantReservation.Domain.Restaurants.Services;
using DomainMenuItem = RestaurantReservation.Domain.MenuItems.Models.MenuItem;

namespace RestaurantReservation.Api.MenuItems.Controllers;

[ApiController]
[Route("api/menuItem")]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemService _menuItemService;
    private readonly IRestaurantService _restaurantService;
    private readonly ILogger<MenuItemController> _logger;

    public MenuItemController(IMenuItemService menuItemService, IRestaurantService restaurantService, ILogger<MenuItemController> logger)
    {
        _menuItemService = menuItemService;
        _restaurantService = restaurantService;
        _logger = logger;
    }

    // New GetMenuItem method
    [HttpGet("{id}", Name = "GetMenuItem")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MenuItemResponse>> GetMenuItem(long id)
    {
        try
        {
            var menuItem = await _menuItemService.GetMenuItemByIdAsync(id);
            return Ok(menuItem.ToResponse());
        }
        catch (EntityNotFoundException<DomainMenuItem>)
        {
            _logger.LogError($"MenuItem with id {id} not found");
            return NotFound($"MenuItem with id {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving menuItem with id {id}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the menuItem");
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MenuItemResponse>>> GetMenuItems()
    {
        try
        {
            var menuItems = await _menuItemService.GetAllMenuItemsAsync();
            return Ok(menuItems.Select(MenuItemMapperDto.ToResponse).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving menuItems");
            return StatusCode(500, "An error occurred while retrieving menuItems");
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MenuItemResponse>> CreateMenuItem([FromBody] MenuItemRequest menuItemRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _logger.Log(LogLevel.Information, $"Creating menuItem");
        try
        {
            var createdMenuItem = await _menuItemService.AddMenuItemAsync(menuItemRequest.ToDomain());
            var response = createdMenuItem.ToResponse();
            return CreatedAtRoute("GetMenuItem", new { id = response.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating menuItem");
            return StatusCode(500, "An error occurred while creating the menuItem");
        }
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateMenuItem(long id, [FromBody] MenuItemRequest menuItemRequest)
    {
        try
        {
            _logger.Log(LogLevel.Information, $"Updating menuItem");
            var menuItem = menuItemRequest.ToDomain();
            menuItem.Id = id;
            await _menuItemService.UpdateMenuItemAsync(menuItem);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"MenuItem with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating menuItem with ID {id}");
            return StatusCode(500, "An error occurred while updating the menuItem");
        }
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteMenuItem(long id)
    {
        try
        {
            await _menuItemService.DeleteMenuItemAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"MenuItem with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting menuItem with ID {id}");
            return StatusCode(500, "An error occurred while deleting the menuItem");
        }
    }
}