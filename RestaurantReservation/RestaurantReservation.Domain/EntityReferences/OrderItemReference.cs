namespace RestaurantReservation.Domain.EntityReferences;

public class OrderItemReference : EntityReference<long>
{
    public int Quantity { get; set; }

    public static OrderItemReference Create(long id, string name, int quantity) => new(){Quantity = quantity, Id = id, Name = name };

    public OrderItemReference WithQuantity(int quantity) => new(){Quantity = quantity, Id = Id, Name = Name };
}