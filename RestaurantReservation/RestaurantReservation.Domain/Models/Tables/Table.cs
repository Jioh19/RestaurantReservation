
using RestaurantReservation.Domain.Models.Restaurants;

namespace RestaurantReservation.Domain.Models.Tables;

public class Table
{
    public long TableId { get; set; }
    public long RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
    public int Capacity { get; set; }
}