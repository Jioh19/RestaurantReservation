using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Domain.Models.Customers;

namespace RestaurantReservation.Infrastructure.Customers.Models;

public class Customer
{
    public long CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

internal class CustomerTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> modelBuilder)
    {
        modelBuilder.ToTable("Customers", "dbo");
        modelBuilder.HasKey(c => c.CustomerId);
        modelBuilder.HasIndex(c => c.Email).IsUnique();
    }
}