
using RestaurantReservation.Api.Contracts.EntityReferences;

namespace RestaurantReservation.Api.Contracts.Tables.Models;

public class TableRequest
{
    public long Id { get; set; }
    public int Capacity { get; set; } 
    public EntityReference<long> Restaurant { get; set; }
}