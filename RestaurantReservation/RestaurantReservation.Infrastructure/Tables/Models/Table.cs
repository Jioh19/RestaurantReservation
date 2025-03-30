using RestaurantReservation.Infrastructure.Restaurants.Models;

namespace RestaurantReservation.Infrastructure.Tables.Models;

public class Table
{
    public long Id { get; set; }
    public virtual Restaurant Restaurant { get; set; }
    public int Capacity { get; set; }
}