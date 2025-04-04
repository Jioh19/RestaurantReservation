using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Infrastructure.Restaurants.Models;

namespace RestaurantReservation.Infrastructure.Employees.Models;

public class Employee
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public long RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;
}

internal class EmployeeTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> modelBuilder)
    {
        modelBuilder.ToTable("Employees", "dbo");
        modelBuilder.HasKey(e => e.Id);
        modelBuilder.Property(e => e.FirstName).HasMaxLength(50);
        modelBuilder.Property(e => e.LastName).HasMaxLength(50);
        modelBuilder.Property(e => e.Position).HasMaxLength(50);
        modelBuilder.HasOne(e => e.Restaurant).WithMany().HasForeignKey(e => e.RestaurantId);
    }
}