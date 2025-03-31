using FluentAssertions;
using RestaurantReservation.Domain.Employees.Models;
using RestaurantReservation.Domain.Orders.Models;
using RestaurantReservation.Domain.Reservations.Models;

namespace RestaurantReservation.Domain.Tests.Models;

public class OrderTests
{
    [Fact]
    public void Order_WithValidData_ShouldBeValid()
    {
        // Arrange
        var reservation = new Reservation
        {
            Id = 1,
        };

        var employee = new Employee
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Position = "Waiter"
        };

        var order = new Order
        {
            Id = 1,
            ReservationId = reservation.Id,
            EmployeeId = employee.Id,
            OrderDate = DateTime.Now,
            TotalAmount = 75.50m
        };

        // Assert
        order.Id.Should().Be(1);
        order.ReservationId.Should().Be(reservation.Id);
        order.EmployeeId.Should().Be(employee.Id);
        order.OrderDate.Should().BeCloseTo(DateTime.Now, precision: TimeSpan.FromSeconds(1));
        order.TotalAmount.Should().Be(75.50m);
    }
}