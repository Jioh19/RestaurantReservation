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
        var customer = new Customer
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
            RestaurantId = restaurant.RestaurantId,
            Capacity = 4
        };

        var reservation = new Reservation
        {
            ReservationId = 1,
            CustomerId = customer.CustomerId,
            Customer = customer,
            RestaurantId = restaurant.RestaurantId,
            Restaurant = restaurant,
            TableId = table.TableId,
            Table = table,
            ReservationDate = DateTime.Now.AddDays(1),
            PartySize = 2
        };

        // Assert
        reservation.ReservationId.Should().Be(1);
        reservation.CustomerId.Should().Be(1);
        reservation.Customer.Should().Be(customer);
        reservation.RestaurantId.Should().Be(1);
        reservation.Restaurant.Should().Be(restaurant);
        reservation.TableId.Should().Be(1);
        reservation.Table.Should().Be(table);
        reservation.ReservationDate.Should().BeCloseTo(DateTime.Now.AddDays(1), precision: TimeSpan.FromSeconds(1));
        reservation.PartySize.Should().Be(2);
    }
}