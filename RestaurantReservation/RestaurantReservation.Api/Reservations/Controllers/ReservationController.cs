using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Api.Contracts.Orders.Models;
using RestaurantReservation.Api.Contracts.Reservations;
using RestaurantReservation.Api.Orders.Mappers;
using RestaurantReservation.Api.Reservations.Mappers;
using RestaurantReservation.Domain.Errors;
using RestaurantReservation.Domain.Orders.Services;
using RestaurantReservation.Domain.Reservations.Services;
using RestaurantReservation.Domain.Restaurants.Services;
using DomainReservation = RestaurantReservation.Domain.Reservations.Models.Reservation;

namespace RestaurantReservation.Api.Reservations.Controllers;

[ApiController]
[Route("api/reservation")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;
    private readonly IOrderService _orderService;
    private readonly ILogger<ReservationController> _logger;

    public ReservationController(IReservationService reservationService, IOrderService orderService, ILogger<ReservationController> logger)
    {
        _reservationService = reservationService;
        _orderService = orderService;
        _logger = logger;
    }

    // New GetReservation method
    [HttpGet("{id}", Name = "GetReservation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReservationResponse>> GetReservation(long id)
    {
        try
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            return Ok(reservation.ToResponse());
        }
        catch (EntityNotFoundException<DomainReservation>)
        {
            _logger.LogError($"Reservation with id {id} not found");
            return NotFound($"Reservation with id {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving reservation with id {id}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the reservation");
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ReservationResponse>>> GetReservations()
    {
        try
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations.Select(ReservationMapperDto.ToResponse).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving reservations");
            return StatusCode(500, "An error occurred while retrieving reservations");
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ReservationResponse>> CreateReservation([FromBody] ReservationRequest reservationRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _logger.Log(LogLevel.Information, $"Creating reservation");
        try
        {
            var createdReservation = await _reservationService.AddReservationAsync(reservationRequest.ToDomain());
            var response = createdReservation.ToResponse();
            return CreatedAtRoute("GetReservation", new { id = response.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating reservation");
            return StatusCode(500, "An error occurred while creating the reservation");
        }
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateReservation(long id, [FromBody] ReservationRequest reservationRequest)
    {
        try
        {
            _logger.Log(LogLevel.Information, $"Updating reservation");
            var reservation = reservationRequest.ToDomain();
            reservation.Id = id;
            await _reservationService.UpdateReservationAsync(reservation);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Reservation with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating reservation with ID {id}");
            return StatusCode(500, "An error occurred while updating the reservation");
        }
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteReservation(long id)
    {
        try
        {
            await _reservationService.DeleteReservationAsync(id);
            return NoContent();
        }
        catch (EntityNotFoundException<DomainReservation>)
        {
            return NotFound($"Reservation with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting reservation with ID {id}");
            return StatusCode(500, "An error occurred while deleting the reservation");
        }
    }
    
    [HttpPost("import")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateBatchReservations([FromBody] IEnumerable<ReservationRequest> reservationRequests)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid reservation data");
        }
        _logger.Log(LogLevel.Information, $"Creating many reservations");
        try
        {
            await _reservationService.AddAllReservationAsync(reservationRequests.Select(e => e.ToDomain()).ToList());
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating reservations");
            return BadRequest("Error creating reservations");
        }
    }
    
    [HttpGet("customer/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ReservationResponse>>> GetReservationsByCustomerId(long id)
    {
        try
        {
            var reservations = await _reservationService.GetReservationsByCustomerIdAsync(id);
            return Ok(reservations.Select(ReservationMapperDto.ToResponse).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving reservations");
            return StatusCode(500, "An error occurred while retrieving reservations");
        }
    }
    
    [HttpGet("{id:long}/orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByReservationId(long id)
    {
        try
        {
            var orders = await _orderService.GetOrdersByReservationIdAsync(id);
            return Ok(orders.Select(OrderMapperDto.ToResponse).ToList());

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving reservations");
            return StatusCode(500, "An error occurred while retrieving reservations");
        }
    }
}