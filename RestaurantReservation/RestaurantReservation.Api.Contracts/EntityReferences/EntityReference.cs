namespace RestaurantReservation.Api.Contracts.EntityReferences;

public record EntityReference<T>(T Id, string Name) where T : struct
{
    public static EntityReference<T> Empty => new(default, string.Empty);
}