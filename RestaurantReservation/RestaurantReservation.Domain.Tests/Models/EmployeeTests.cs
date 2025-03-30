using FluentAssertions;
using RestaurantReservation.Domain.Models.Employees;
using RestaurantReservation.Domain.Restaurants.Models;

namespace RestaurantReservation.Domain.Tests.Models
{
    public class EmployeeTests
    {
        [Fact]
        public void Employee_WithValidData_ShouldBeValid()
        {
            // Arrange
            var restaurant = new Restaurant
            {
                Id = 1,
                Name = "Test Restaurant"
            };

            var employee = new Employee
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Position = "Waiter",
                Restaurant = restaurant,
            };

            // Assert
            employee.Id.Should().Be(1);
            employee.FirstName.Should().Be("John");
            employee.LastName.Should().Be("Doe");
            employee.Position.Should().Be("Waiter");
            employee.Restaurant.Should().Be(restaurant);
        }
    }
}