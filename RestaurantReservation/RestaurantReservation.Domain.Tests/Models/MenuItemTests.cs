using FluentAssertions;
using FluentAssertions.Execution;
using RestaurantReservation.Domain.MenuItems.Models;
using RestaurantReservation.Domain.Restaurants.Models;

namespace RestaurantReservation.Domain.Tests.Models;

public class MenuItemTests
{
    [Fact]
    public void MenuItem_WithValidData_ShouldBeValid()
    {
        // Arrange
        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Test Restaurant"
        };

        var menuItem = new MenuItem
        {
            Id = 1,
            RestaurantId = restaurant.Id,
            Name = "Margherita Pizza",
            Description = "Classic Italian pizza with tomato and mozzarella",
            Price = 12.99m
        };

        // Assert
        using (new AssertionScope())
        {
            menuItem.Id.Should().Be(1);
            menuItem.RestaurantId.Should().Be(restaurant.Id);
            menuItem.Name.Should().Be("Margherita Pizza");
            menuItem.Description.Should().Be("Classic Italian pizza with tomato and mozzarella");
            menuItem.Price.Should().Be(12.99m);
        }
    }
}