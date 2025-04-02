using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Api.Contracts.Orders.Models;
using RestaurantReservation.Api.Orders.Mappers;
using RestaurantReservation.Domain.Errors;
using RestaurantReservation.Domain.Orders.Services;
using RestaurantReservation.Domain.Restaurants.Services;
using DomainOrder = RestaurantReservation.Domain.Orders.Models.Order;

namespace RestaurantReservation.Api.Orders.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IRestaurantService _restaurantService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderService orderService, IRestaurantService restaurantService, ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _restaurantService = restaurantService;
        _logger = logger;
    }

    // New GetOrder method
    [HttpGet("{id}", Name = "GetOrder")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderResponse>> GetOrder(long id)
    {
        try
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return Ok(order.ToResponse());
        }
        catch (EntityNotFoundException<DomainOrder>)
        {
            _logger.LogError($"Order with id {id} not found");
            return NotFound($"Order with id {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving order with id {id}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the order");
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrders()
    {
        try
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders.Select(OrderMapperDto.ToResponse).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving orders");
            return StatusCode(500, "An error occurred while retrieving orders");
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OrderResponse>> CreateOrder([FromBody] OrderRequest orderRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _logger.Log(LogLevel.Information, $"Creating order");
        try
        {
            var createdOrder = await _orderService.AddOrderAsync(orderRequest.ToDomain());
            var response = createdOrder.ToResponse();
            return CreatedAtRoute("GetOrder", new { id = response.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order");
            return StatusCode(500, "An error occurred while creating the order");
        }
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateOrder(long id, [FromBody] OrderRequest orderRequest)
    {
        try
        {
            _logger.Log(LogLevel.Information, $"Updating order");
            var order = orderRequest.ToDomain();
            order.Id = id;
            await _orderService.UpdateOrderAsync(order);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Order with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating order with ID {id}");
            return StatusCode(500, "An error occurred while updating the order");
        }
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteOrder(long id)
    {
        try
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
        catch (EntityNotFoundException<DomainOrder>)
        {
            return NotFound($"Order with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting order with ID {id}");
            return StatusCode(500, "An error occurred while deleting the order");
        }
    }
    
    [HttpPost("import")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateBatchOrders([FromBody] IEnumerable<OrderRequest> orderRequests)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid order data");
        }
        _logger.Log(LogLevel.Information, $"Creating many orders");
        try
        {
            await _orderService.AddAllOrderAsync(orderRequests.Select(e => e.ToDomain()).ToList());
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating orders");
            return BadRequest("Error creating orders");
        }
    }
}