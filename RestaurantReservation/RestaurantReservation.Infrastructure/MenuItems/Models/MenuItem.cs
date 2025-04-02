using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Infrastructure.Restaurants.Models;

namespace RestaurantReservation.Infrastructure.MenuItems.Models;

public class MenuItem
{
    public long Id { get; set; }
    public long RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    public decimal Price { get; set; }
}

internal class MenuItemTypeConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> modelBuilder)
    {
        modelBuilder.ToTable("MenuItems", "dbo");
        modelBuilder.HasKey(m => m.Id);
        modelBuilder.HasOne(m => m.Restaurant).WithMany().HasForeignKey(m => m.RestaurantId);
        modelBuilder.Property(m => m.Name).HasMaxLength(50).IsRequired();
        modelBuilder.Property(m => m.Description).HasMaxLength(200).IsRequired();
        modelBuilder.Property(m => m.Price).HasColumnType("decimal(18,2)");
    }
}