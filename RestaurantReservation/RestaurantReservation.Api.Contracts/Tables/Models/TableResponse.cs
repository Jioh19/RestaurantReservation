using RestaurantReservation.Domain.Restaurants.Models;

namespace RestaurantReservation.Api.Contracts.Tables.Models;

public class TableResponse
{
    public long Id { get; set; }
    public long RestaurantId { get; set; }
    public int Capacity { get; set; }
}