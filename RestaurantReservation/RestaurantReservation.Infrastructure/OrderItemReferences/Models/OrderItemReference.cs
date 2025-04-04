using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Infrastructure.MenuItems.Models;
using RestaurantReservation.Infrastructure.Orders.Models;

namespace RestaurantReservation.Infrastructure.OrderItemReferences.Models;

public class OrderItemReference
{
    public long Id { get; set; }
    public int Quantity { get; set; }
    public long MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; } = null!;
    public long OrderId { get; set; }
    public Order Order { get; set; } = null!;
}

internal class OrderItemReferenceTypeConfiguration : IEntityTypeConfiguration<OrderItemReference>
{
    public void Configure(EntityTypeBuilder<OrderItemReference> modelBuilder)
    {
        modelBuilder.ToTable("OrderItemReference", "dbo");
        modelBuilder.HasKey(o => o.Id);
        modelBuilder.Property(o => o.Quantity).IsRequired();
        modelBuilder.HasOne(o => o.MenuItem).WithMany().HasForeignKey(o => o.MenuItemId);
        modelBuilder.HasOne(o => o.Order).WithMany().HasForeignKey(o => o.OrderId);

    }
}