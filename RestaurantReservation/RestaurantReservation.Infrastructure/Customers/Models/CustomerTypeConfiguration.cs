using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Domain.Models.Customers;

namespace RestaurantReservation.Infrastructure.Customers.Models;

public class CustomerTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> modelBuilder)
    {
        modelBuilder.ToTable("Customers", "dbo");
        modelBuilder.HasKey(c => c.CustomerId);
        
        // Indexes
        modelBuilder.HasIndex(c => c.Email).IsUnique();
    }
}