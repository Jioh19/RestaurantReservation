using FluentAssertions.Execution;
using RestaurantReservation.Domain.Employees.Models;
using RestaurantReservation.Domain.EntityReferences;
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
                Restaurant = new EntityReference<long>(){ Id = restaurant.Id, Name = restaurant.Name },
            };

            // Assert
            using (new AssertionScope())
            {
                employee.Id.Should().Be(1);
                employee.FirstName.Should().Be("John");
                employee.LastName.Should().Be("Doe");
                employee.Position.Should().Be("Waiter");
                employee.Restaurant.Id.Should().Be(restaurant.Id);
            }
        }
    }
}