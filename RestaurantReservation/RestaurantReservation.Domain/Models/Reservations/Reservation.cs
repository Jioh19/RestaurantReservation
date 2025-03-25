
using RestaurantReservation.Domain.Models.Customers;
using RestaurantReservation.Domain.Models.Restaurants;
using RestaurantReservation.Domain.Models.Tables;

namespace RestaurantReservation.Domain.Models.Reservations;

public class Reservation
{
    public long ReservationId { get; set; }
    public long CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public long RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
    public long TableId { get; set; }
    public virtual Table Table { get; set; }
    public DateTime ReservationDate { get; set; }
    public int PartySize { get; set; }
}