using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Infrastructure.OrderItemReferences.Models;
using RestaurantReservation.Infrastructure.Employees.Models;
using RestaurantReservation.Infrastructure.MenuItems.Models;
using RestaurantReservation.Infrastructure.Reservations.Models;

namespace RestaurantReservation.Infrastructure.Orders.Models;

public class Order
{
    public long Id { get; set; }
    public long ReservationId { get; set; }
    public Reservation Reservation { get; set; } = null!;
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    
    public IReadOnlyList<MenuItem> OrderItems { get; set; } = Array.Empty<MenuItem>();
}

internal class OrderTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> modelBuilder)
    {
        modelBuilder.ToTable("Orders", "dbo");
        modelBuilder.HasKey(o => o.Id);
        modelBuilder.HasOne(o => o.Reservation).WithMany().HasForeignKey(o => o.ReservationId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.HasOne(o => o.Employee).WithMany().HasForeignKey(o => o.EmployeeId);

        modelBuilder.HasMany(o => o.OrderItems).WithMany().UsingEntity<OrderItemReference>();
        
        modelBuilder.Property(o => o.OrderDate).HasColumnType("datetime").IsRequired();
        modelBuilder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
    }
}