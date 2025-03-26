using FluentAssertions;
using RestaurantReservation.Domain.Models.Restaurants;

namespace RestaurantReservation.Domain.Tests.Models;

public class RestaurantTests
{
    [Fact]
    public void Restaurant_WithValidData_ShouldBeValid()
    {
        // Arrange
        var restaurant = new Restaurant
        {
            RestaurantId = 1,
            Name = "McDowels",
            Address = "123 Main Street, Cityville",
            PhoneNumber = "+1234567890",
            OpeningHours = new TimeOnly(10, 0) // 10:00 AM
        };

        // Assert
        restaurant.RestaurantId.Should().Be(1);
        restaurant.Name.Should().Be("McDowels");
        restaurant.Address.Should().Be("123 Main Street, Cityville");
        restaurant.PhoneNumber.Should().Be("+1234567890");
        restaurant.OpeningHours.Should().Be(new TimeOnly(10, 0));
    }
}