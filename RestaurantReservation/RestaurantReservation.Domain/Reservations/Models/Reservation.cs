using RestaurantReservation.Domain.Customers.Models;
using RestaurantReservation.Domain.Restaurants.Models;
using RestaurantReservation.Domain.Tables.Models;

namespace RestaurantReservation.Domain.Reservations.Models;

public class Reservation
{
    public long Id { get; set; }
    
    public long CustomerId { get; set; }
    
    public long RestaurantId { get; set; }
    
    public long TableId { get; set; }
    
    public DateTime ReservationDate { get; set; }
    
    public int PartySize { get; set; }
}