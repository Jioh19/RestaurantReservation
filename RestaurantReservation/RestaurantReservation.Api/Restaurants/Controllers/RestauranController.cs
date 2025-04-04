using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Api.Contracts.Restaurants.Models;
using RestaurantReservation.Api.Restaurants.Mappers;
using RestaurantReservation.Domain.Errors;
using RestaurantReservation.Domain.Restaurants.Services;
using DomainRestaurant = RestaurantReservation.Domain.Restaurants.Models.Restaurant;

namespace RestaurantReservation.Api.Restaurants.Controllers;

[ApiController]
[Route("api/restaurant")]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;
    private readonly ILogger<RestaurantController> _logger;
    private readonly IValidator<RestaurantRequest> _restaurantValidator;

    public RestaurantController(IRestaurantService restaurantService, ILogger<RestaurantController> logger, IValidator<RestaurantRequest> restaurantRequestValidator)
    {
        _restaurantService = restaurantService;
        _logger = logger;
        _restaurantValidator = restaurantRequestValidator;
    }

    [HttpGet("{id:long}", Name = "GetRestaurant")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RestaurantResponse>> GetRestaurant(long id)
    {
        try
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            return Ok(restaurant.ToResponse());
        }
        catch (EntityNotFoundException<DomainRestaurant>)
        {
            _logger.LogError($"Restaurant with id {id} not found");
            return NotFound($"Restaurant with id {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving restaurant with id {id}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the restaurant");
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RestaurantResponse>> GetRestaurants()
    {
        try
        {
            var restaurants = await _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurants.Select(RestaurantMapperDto.ToResponse).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving restaurants");
            return StatusCode(500, "An error occurred while retrieving the restaurants");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RestaurantResponse>> CreateRestaurant([FromBody] RestaurantRequest restaurantRequest)
    {
        var result = await _restaurantValidator.ValidateAsync(restaurantRequest);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        _logger.Log(LogLevel.Information, $"Creating restaurant {restaurantRequest.Name}");
        try
        {
            var createdRestaurant = await _restaurantService.AddRestaurantAsync(restaurantRequest.ToDomain());
            return CreatedAtRoute("GetRestaurant", new { id = createdRestaurant.Id }, createdRestaurant.ToResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating restaurant");
            return StatusCode(500, "An error occurred while creating the restaurant");
        }
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateRestaurant(long id, [FromBody] RestaurantRequest restaurantRequest)
    {
        
        var result = await _restaurantValidator.ValidateAsync(restaurantRequest);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }
        
        try
        {
            restaurantRequest.Id = id;
            await _restaurantService.UpdateRestaurantAsync(restaurantRequest.ToDomain());
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Restaurant with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating restaurant with ID {id}");
            return StatusCode(500, "An error occurred while updating the restaurant");
        }
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteRestaurant(long id)
    {
        try
        {
            await _restaurantService.DeleteRestaurantAsync(id);
            return NoContent();
        }
        catch (EntityNotFoundException<DomainRestaurant>)
        {
            return NotFound($"Restaurant with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting restaurant with ID {id}");
            return StatusCode(500, "An error occurred while deleting the restaurant");
        }
    }
}