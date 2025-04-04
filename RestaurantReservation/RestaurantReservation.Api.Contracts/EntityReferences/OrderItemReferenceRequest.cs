namespace RestaurantReservation.Api.Contracts.OrderItemReference;

public class OrderItemReferenceRequest
{
    public long Id { get; set; }
    public long ItemId { get; set; }
    public int Quantity { get; set; }
}