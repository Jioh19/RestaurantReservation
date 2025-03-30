
using RestaurantReservation.Domain.Restaurants.Models;

namespace RestaurantReservation.Domain.Tables.Models;

public class Table
{
    public long Id { get; set; }
    public virtual Restaurant Restaurant { get; set; }
    public int Capacity { get; set; }
}