using FluentAssertions;
using RestaurantReservation.Domain.Employees.Models;
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
                RestaurantId = restaurant.Id,
            };

            // Assert
            employee.Id.Should().Be(1);
            employee.FirstName.Should().Be("John");
            employee.LastName.Should().Be("Doe");
            employee.Position.Should().Be("Waiter");
            employee.RestaurantId.Should().Be(restaurant.Id);
        }
    }
}