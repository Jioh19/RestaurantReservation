using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RestaurantReservation.Infrastructure.Restaurants.Models;

public class Restaurant
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; } 
    public string PhoneNumber { get; set; }
    public TimeOnly OpeningHours { get; set; }

    internal class RestaurantTypeConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> modelBuilder)
        {
            modelBuilder.ToTable("Restaurants", "dbo");
            modelBuilder.HasKey(r => r.Id);
            modelBuilder.Property(r => r.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Property(r => r.Address).HasMaxLength(50).IsRequired();
            modelBuilder.Property(r => r.PhoneNumber).HasMaxLength(10).IsRequired();
            modelBuilder.Property(r => r.OpeningHours).HasColumnType("time");
        }
    }
}