using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RestaurantReservation.Infrastructure.Customers.Models;

public class Customer
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}

internal class CustomerTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> modelBuilder)
    {
        modelBuilder.ToTable("Customers", "dbo");
        modelBuilder.HasKey(c => c.Id);
        modelBuilder.HasIndex(c => c.Email).IsUnique();
        modelBuilder.Property(x => x.FirstName).HasMaxLength(50);
        modelBuilder.Property(x => x.LastName).HasMaxLength(50);
        modelBuilder.Property(x => x.Email).HasMaxLength(50);
        modelBuilder.Property(x => x.PhoneNumber).HasMaxLength(10);
    }
}