using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Api.Contracts.OrderItemReference;
using RestaurantReservation.Api.OrderItemReference.Mappers;
using RestaurantReservation.Domain.EntityReferences.Service;
using RestaurantReservation.Domain.Errors;
using DomainOrderItemReference = RestaurantReservation.Domain.EntityReferences.OrderItemReference;

namespace RestaurantReservation.Api.OrderItemReference;

[ApiController]
[Route("api/orderItemReference")]
public class OrderItemReferenceController : ControllerBase
{
    private readonly IOrderItemReferenceService _orderItemReferenceService;
    private readonly ILogger<OrderItemReferenceController> _logger;

    public OrderItemReferenceController(IOrderItemReferenceService orderItemReferenceService, ILogger<OrderItemReferenceController> logger)
    {
        _orderItemReferenceService = orderItemReferenceService;
        _logger = logger;
    }
    
    // New GetOrderItemReference method
    [HttpGet("{id}", Name = "GetOrderItemReference")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderItemReferenceResponse>> GetOrderItemReference(long id)
    {
        try
        {
            var orderItemReference = await _orderItemReferenceService.GetOrderItemReferenceByIdAsync(id);
            return Ok(orderItemReference.ToResponse());
        }
        catch (EntityNotFoundException<DomainOrderItemReference>)
        {
            _logger.LogError($"OrderItemReference with id {id} not found");
            return NotFound($"OrderItemReference with id {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving orderItemReference with id {id}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the orderItemReference");
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<OrderItemReferenceResponse>>> GetOrderItemReferences()
    {
        try
        {
            var orderItemReferences = await _orderItemReferenceService.GetAllOrderItemReferencesAsync();
            return Ok(orderItemReferences.Select(OrderItemReferenceMapper.ToResponse).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving orderItemReferences");
            return StatusCode(500, "An error occurred while retrieving orderItemReferences");
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OrderItemReferenceResponse>> CreateOrderItemReference([FromBody] OrderItemReferenceRequest orderItemReferenceRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var createdOrderItemReference = await _orderItemReferenceService.AddOrderItemReferenceAsync(orderItemReferenceRequest.ToDomain());
            return CreatedAtRoute("GetOrderItemReference", new { id = createdOrderItemReference.Id }, createdOrderItemReference.ToResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating orderItemReference");
            return StatusCode(500, "An error occurred while creating the orderItemReference");
        }
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateOrderItemReference(long id, [FromBody] OrderItemReferenceRequest orderItemReferenceRequest)
    {
        try
        {
            orderItemReferenceRequest.Id = id;
            await _orderItemReferenceService.UpdateOrderItemReferenceAsync(orderItemReferenceRequest.ToDomain());
            return NoContent();
        }
        catch (EntityNotFoundException<DomainOrderItemReference>)
        {
            return NotFound($"OrderItemReference with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating orderItemReference with ID {id}");
            return StatusCode(500, "An error occurred while updating the orderItemReference");
        }
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteOrderItemReference(long id)
    {
        try
        {
            await _orderItemReferenceService.DeleteOrderItemReferenceAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"OrderItemReference with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting orderItemReference with ID {id}");
            return StatusCode(500, "An error occurred while deleting the orderItemReference");
        }
    }
}