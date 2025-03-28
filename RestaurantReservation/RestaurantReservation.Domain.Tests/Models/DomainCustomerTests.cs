using FluentAssertions;
using FluentAssertions.Execution;
using RestaurantReservation.Domain.Models.Customers;

namespace RestaurantReservation.Domain.Tests.Models
{
    public class DomainCustomerTests
    {
        [Fact]
        public void Customer_WithValidData_ShouldBeValid()
        {
            // Arrange
            var customer = new DomainCustomer
            {
                CustomerId = 1,
                FirstName = "Juan",
                LastName = "Oh",
                Email = "jioh@mail.com",
                PhoneNumber = "+1234567890"
            };

            // Act & Assert
            using (new AssertionScope())
            {
                customer.CustomerId.Should().Be(1);
                customer.FirstName.Should().Be("Juan");
                customer.LastName.Should().Be("Oh");
                customer.Email.Should().Be("jioh@mail.com");
                customer.PhoneNumber.Should().Be("+1234567890");
            }
        }
        
    }
}