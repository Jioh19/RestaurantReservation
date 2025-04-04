namespace RestaurantReservation.Api.Contracts.EntityReferences;

public class OrderItemReferenceResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
}