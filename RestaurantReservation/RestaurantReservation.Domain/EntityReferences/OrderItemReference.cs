namespace RestaurantReservation.Domain.EntityReferences;

public class OrderItemReference : EntityReference
{
    public int Quantity { get; set; }
    public long ItemId { get; set; }

    public static OrderItemReference Create(long itemId, int quantity)
    {
        return new OrderItemReference
        {
            ItemId = itemId,
            Quantity = quantity,
        };
    }

    public OrderItemReference WithQuantity(int quantity)
    {
        return new OrderItemReference
        {
            Id = this.Id,
            Quantity = quantity,
        };
    }
}