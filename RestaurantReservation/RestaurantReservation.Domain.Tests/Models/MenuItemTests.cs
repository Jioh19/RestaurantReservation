using FluentAssertions;
using FluentAssertions.Execution;
using RestaurantReservation.Domain.Models.MenuItems;
using RestaurantReservation.Domain.Models.Restaurants;

namespace RestaurantReservation.Domain.Tests.Models;

public class MenuItemTests
{
    [Fact]
    public void MenuItem_WithValidData_ShouldBeValid()
    {
        // Arrange
        var restaurant = new Restaurant
        {
            RestaurantId = 1,
            Name = "Test Restaurant"
        };

        var menuItem = new MenuItem
        {
            MenuItemId = 1,
            Restaurant = restaurant,
            Name = "Margherita Pizza",
            Description = "Classic Italian pizza with tomato and mozzarella",
            Price = 12.99m
        };

        // Assert
        using (new AssertionScope())
        {
            menuItem.MenuItemId.Should().Be(1);
            menuItem.Restaurant.Should().BeEquivalentTo(restaurant);
            menuItem.Name.Should().Be("Margherita Pizza");
            menuItem.Description.Should().Be("Classic Italian pizza with tomato and mozzarella");
            menuItem.Price.Should().Be(12.99m);
        }
    }
}