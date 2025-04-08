using RestaurantReservation.Domain.EntityReferences;

namespace RestaurantReservation.Domain.Tables.Models;

public class Table
{
    public long Id { get; set; }
    public int Capacity { get; set; }
    public EntityReference<long> Restaurant { get; set; } = EntityReference<long>.Empty;
}