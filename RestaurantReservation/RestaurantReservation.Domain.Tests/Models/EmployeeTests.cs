using FluentAssertions;
using RestaurantReservation.Domain.Models.Employees;
using RestaurantReservation.Domain.Models.Restaurants;

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
                RestaurantId = 1,
                Name = "Test Restaurant"
            };

            var employee = new Employee
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Doe",
                Position = "Waiter",
                RestaurantId = restaurant.RestaurantId,
                Restaurant = restaurant,
            };

            // Assert
            employee.EmployeeId.Should().Be(1);
            employee.FirstName.Should().Be("John");
            employee.LastName.Should().Be("Doe");
            employee.Position.Should().Be("Waiter");
            employee.RestaurantId.Should().Be(1);
            employee.Restaurant.Should().Be(restaurant);
        }
    }
}