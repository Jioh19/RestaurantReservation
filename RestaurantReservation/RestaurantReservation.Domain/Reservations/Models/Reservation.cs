using RestaurantReservation.Domain.Customers.Models;
using RestaurantReservation.Domain.EntityReferences;
using RestaurantReservation.Domain.Restaurants.Models;
using RestaurantReservation.Domain.Tables.Models;

namespace RestaurantReservation.Domain.Reservations.Models;

public class Reservation
{
    public long Id { get; set; }
    
    public DateTime ReservationDate { get; set; }
    
    public int PartySize { get; set; }
    public EntityReference<long> Customer { get; set; } = EntityReference<long>.Empty;
    
    public EntityReference<long> Restaurant { get; set; } = EntityReference<long>.Empty;
    
    public EntityReference<long> Table { get; set; } = EntityReference<long>.Empty;
}