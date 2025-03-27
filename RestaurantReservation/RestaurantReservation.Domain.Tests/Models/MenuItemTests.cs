using FluentAssertions;
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
            ItemId = 1,
            RestaurantId = restaurant.RestaurantId,
            Restaurant = restaurant,
            Name = "Margherita Pizza",
            Description = "Classic Italian pizza with tomato and mozzarella",
            Price = 12.99m
        };

        // Assert
        menuItem.ItemId.Should().Be(1);
        menuItem.RestaurantId.Should().Be(1);
        menuItem.Restaurant.Should().Be(restaurant);
        menuItem.Name.Should().Be("Margherita Pizza");
        menuItem.Description.Should().Be("Classic Italian pizza with tomato and mozzarella");
        menuItem.Price.Should().Be(12.99m);
    }
}