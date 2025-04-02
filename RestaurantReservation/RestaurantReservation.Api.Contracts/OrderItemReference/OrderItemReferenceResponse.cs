namespace RestaurantReservation.Api.Contracts.OrderItemReference;

public class OrderItemReferenceResponse
{
    public long Id { get; set; }
    public long ItemId { get; set; }
    public int Quantity { get; set; }
}