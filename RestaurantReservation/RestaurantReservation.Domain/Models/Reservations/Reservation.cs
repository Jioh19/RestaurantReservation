
using RestaurantReservation.Domain.Customers.Models;
using RestaurantReservation.Domain.Models.Tables;
using RestaurantReservation.Domain.Restaurants.Models;

namespace RestaurantReservation.Domain.Models.Reservations;

public class Reservation
{
    public long ReservationId { get; set; }
    
    public Customer Customer { get; set; }
    
    public Restaurant Restaurant { get; set; }
    
    public Table Table { get; set; }
    
    public DateTime ReservationDate { get; set; }
    
    public int PartySize { get; set; }
}