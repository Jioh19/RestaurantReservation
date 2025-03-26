using FluentAssertions;
using RestaurantReservation.Domain.Models.Employees;
using RestaurantReservation.Domain.Models.Orders;
using RestaurantReservation.Domain.Models.Reservations;

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
            CustomerId = 1,
            RestaurantId = 1
        };

        var employee = new Employee
        {
            EmployeeId = 1,
            FirstName = "John",
            LastName = "Doe",
            Position = "Waiter"
        };

        var order = new Order
        {
            OrderId = 1,
            ReservationId = reservation.ReservationId,
            Reservation = reservation,
            EmployeeId = employee.EmployeeId,
            Employee = employee,
            OrderDate = DateTime.Now,
            TotalAmount = 75.50m
        };

        // Assert
        order.OrderId.Should().Be(1);
        order.ReservationId.Should().Be(1);
        order.Reservation.Should().Be(reservation);
        order.EmployeeId.Should().Be(1);
        order.Employee.Should().Be(employee);
        order.OrderDate.Should().BeCloseTo(DateTime.Now, precision: TimeSpan.FromSeconds(1));
        order.TotalAmount.Should().Be(75.50m);
    }
}