
using RestaurantReservation.Domain.Restaurants.Models;

namespace RestaurantReservation.Domain.Tables.Models;

public class Table
{
    public long Id { get; set; }
    public  long RestaurantId { get; set; }
    public int Capacity { get; set; }

}