namespace RestaurantReservation.Domain.EntityReferences;

public class OrderItemReference : EntityReference
{
    public int Quantity { get; set; }
    public long ItemId { get; set; }

    public OrderItemReference(int quantity, long itemId)
    {
        Quantity = quantity;
        ItemId = itemId;
    }
    
    public static OrderItemReference Create(long itemId, int quantity) => new(quantity, itemId);

    public OrderItemReference WithQuantity(int quantity) => new(quantity, this.ItemId);
}