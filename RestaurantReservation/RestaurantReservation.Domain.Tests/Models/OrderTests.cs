using FluentAssertions;
using RestaurantReservation.Domain.Employees.Models;
using RestaurantReservation.Domain.Models.Orders;
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
            ReservationId = 1,
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
            OrderId = 1,
            Reservation = reservation,
            Employee = employee,
            OrderDate = DateTime.Now,
            TotalAmount = 75.50m
        };

        // Assert
        order.OrderId.Should().Be(1);
        order.Reservation.Should().Be(reservation);
        order.Employee.Should().Be(employee);
        order.OrderDate.Should().BeCloseTo(DateTime.Now, precision: TimeSpan.FromSeconds(1));
        order.TotalAmount.Should().Be(75.50m);
    }
}