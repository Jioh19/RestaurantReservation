using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Domain.EntityReferences;
using RestaurantReservation.Infrastructure.Employees.Models;
using RestaurantReservation.Infrastructure.Reservations.Models;

namespace RestaurantReservation.Infrastructure.Orders.Models;

public class Order
{
    public long Id { get; set; }
    public long ReservationId { get; set; }
    public Reservation Reservation { get; set; }
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    
    public IReadOnlyList<OrderItemReference> OrderItems { get; set; } = Array.Empty<OrderItemReference>();
}

internal class OrderTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> modelBuilder)
    {
        modelBuilder.ToTable("Orders", "dbo");
        modelBuilder.HasKey(o => o.Id);
        modelBuilder.HasOne(o => o.Reservation).WithMany().HasForeignKey(o => o.ReservationId);
        modelBuilder.HasOne(o => o.Employee).WithMany().HasForeignKey(o => o.EmployeeId);
        modelBuilder.Property(o => o.OrderDate).HasColumnType("datetime").IsRequired();
        modelBuilder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
    }
}