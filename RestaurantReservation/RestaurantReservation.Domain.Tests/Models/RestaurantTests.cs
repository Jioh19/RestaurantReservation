
using RestaurantReservation.Domain.Restaurants.Models;

namespace RestaurantReservation.Domain.Tests.Models;

public class RestaurantTests
{
    [Fact]
    public void Restaurant_WithValidData_ShouldBeValid()
    {
        // Arrange
        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "McDowels",
            Address = "123 Main Street, Cityville",
            PhoneNumber = "+1234567890",
            OpeningHours = new TimeOnly(10, 0) // 10:00 AM
        };

        // Assert
        restaurant.Id.Should().Be(1);
        restaurant.Name.Should().Be("McDowels");
        restaurant.Address.Should().Be("123 Main Street, Cityville");
        restaurant.PhoneNumber.Should().Be("+1234567890");
        restaurant.OpeningHours.Should().Be(new TimeOnly(10, 0));
    }
}