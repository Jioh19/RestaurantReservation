using RestaurantReservation.Domain.Customers.Models;
using RestaurantReservation.Domain.Reservations.Models;
using RestaurantReservation.Domain.Tables.Models;
using RestaurantReservation.Domain.Restaurants.Models;

namespace RestaurantReservation.Domain.Tests.Models;

public class ReservationTests
{
    [Fact]
    public void Reservation_WithValidData_ShouldBeValid()
    {
        // Arrange
        var customer = new Customer
        {
            Id = 1,
            FirstName = "Juan",
            LastName = "Oh"
        };

        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Test Restaurant"
        };

        var table = new Table
        {
            Id = 1,
            Capacity = 4
        };

        var reservation = new Reservation
        {
            Id = 1,
            CustomerId = customer.Id,
            RestaurantId = restaurant.Id,
            TableId = table.Id,
            ReservationDate = DateTime.Now.AddDays(1),
            PartySize = 2
        };

        // Assert
        reservation.Id.Should().Be(1);
        reservation.CustomerId.Should().Be(customer.Id);
        reservation.RestaurantId.Should().Be(restaurant.Id);
        reservation.TableId.Should().Be(table.Id);
        reservation.ReservationDate.Should().BeCloseTo(DateTime.Now.AddDays(1), precision: TimeSpan.FromSeconds(1));
        reservation.PartySize.Should().Be(2);
    }
}