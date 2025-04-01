
using RestaurantReservation.Domain.Tables.Models;
using RestaurantReservation.Domain.Restaurants.Models;

namespace RestaurantReservation.Domain.Tests.Models;

public class TableTests
{
    [Fact]
    public void Table_WithValidData_ShouldBeValid()
    {
        // Arrange
        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Test Restaurant"
        };

        var table = new Table
        {
            Id = 1,
            RestaurantId = restaurant.Id,
            Capacity = 4
        };

        // Assert
        table.Id.Should().Be(1);
        table.RestaurantId.Should().Be(restaurant.Id);
        table.Capacity.Should().Be(4);
    }
}