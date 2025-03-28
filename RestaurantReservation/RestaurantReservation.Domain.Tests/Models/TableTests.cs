using FluentAssertions;
using RestaurantReservation.Domain.Models.Restaurants;
using RestaurantReservation.Domain.Models.Tables;

namespace RestaurantReservation.Domain.Tests.Models;

public class TableTests
{
    [Fact]
    public void Table_WithValidData_ShouldBeValid()
    {
        // Arrange
        var restaurant = new Restaurant
        {
            RestaurantId = 1,
            Name = "Test Restaurant"
        };

        var table = new Table
        {
            TableId = 1,
            Restaurant = restaurant,
            Capacity = 4
        };

        // Assert
        table.TableId.Should().Be(1);
        table.Restaurant.Should().Be(restaurant);
        table.Capacity.Should().Be(4);
    }
}