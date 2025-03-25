namespace RestaurantReservation.Domain.EntityReferences;

public abstract class EntityReference
{
    public long Id { get; set; }

    public override bool Equals(object? obj) =>
        obj is EntityReference reference && Id == reference.Id;

    public override int GetHashCode() =>
        Id.GetHashCode();
}