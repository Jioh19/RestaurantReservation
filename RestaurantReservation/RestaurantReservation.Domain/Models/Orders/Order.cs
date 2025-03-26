using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantReservation.Domain.Models.Employees;
using RestaurantReservation.Domain.Models.Reservations;

namespace RestaurantReservation.Domain.Models.Orders;

public class Order
{
    public long OrderId { get; set; }
    public long ReservationId { get; set; }
    public virtual Reservation Reservation { get; set; }
    public long EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    
}