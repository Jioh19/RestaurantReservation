using FluentAssertions;
using RestaurantReservation.Domain.Models.Customers;
using RestaurantReservation.Domain.Models.Reservations;
using RestaurantReservation.Domain.Models.Restaurants;
using RestaurantReservation.Domain.Models.Tables;

namespace RestaurantReservation.Domain.Tests.Models;

public class ReservationTests
{
    [Fact]
    public void Reservation_WithValidData_ShouldBeValid()
    {
        // Arrange
        var customer = new DomainCustomer
        {
            CustomerId = 1,
            FirstName = "Juan",
            LastName = "Oh"
        };

        var restaurant = new Restaurant
        {
            RestaurantId = 1,
            Name = "Test Restaurant"
        };

        var table = new Table
        {
            TableId = 1,
            Capacity = 4
        };

        var reservation = new Reservation
        {
            ReservationId = 1,
            DomainCustomer = customer,
            Restaurant = restaurant,
            Table = table,
            ReservationDate = DateTime.Now.AddDays(1),
            PartySize = 2
        };

        // Assert
        reservation.ReservationId.Should().Be(1);
        reservation.DomainCustomer.Should().Be(customer);
        reservation.Restaurant.Should().Be(restaurant);
        reservation.Table.Should().Be(table);
        reservation.ReservationDate.Should().BeCloseTo(DateTime.Now.AddDays(1), precision: TimeSpan.FromSeconds(1));
        reservation.PartySize.Should().Be(2);
    }
}