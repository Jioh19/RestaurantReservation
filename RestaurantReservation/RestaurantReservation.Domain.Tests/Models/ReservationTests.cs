using RestaurantReservation.Domain.Customers.Models;
using RestaurantReservation.Domain.EntityReferences;
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
        var customer = new EntityReference<long>()
        {
            Id = 1,
            Name = "Test Customer"
        };

        var restaurant =new EntityReference<long>()
        {
            Id = 1,
            Name = "Test Restaurant"
        };

        var table = new EntityReference<long>()
        {
            Id = 1,
            Name = "Test Table"
        };

        var reservation = new Reservation
        {
            Id = 1,
            Customer = customer,
            Restaurant = restaurant,
            Table = table,
            ReservationDate = DateTime.Now.AddDays(1),
            PartySize = 2
        };

        // Assert
        reservation.Id.Should().Be(1);
        reservation.Customer.Should().Be(customer);
        reservation.Restaurant.Should().Be(restaurant);
        reservation.Table.Should().Be(table);
        reservation.ReservationDate.Should().BeCloseTo(DateTime.Now.AddDays(1), precision: TimeSpan.FromSeconds(1));
        reservation.PartySize.Should().Be(2);
    }
}