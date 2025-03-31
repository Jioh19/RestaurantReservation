using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Infrastructure.Customers.Models;
using RestaurantReservation.Infrastructure.Restaurants.Models;
using RestaurantReservation.Infrastructure.Tables.Models;

namespace RestaurantReservation.Infrastructure.Reservations.Models;

public class Reservation
{
    public long Id { get; set; }
    
    public long CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    
    public long RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    
    public long TableId { get; set; }
    public Table Table { get; set; }
    public DateTime ReservationDate { get; set; }
    
    public int PartySize { get; set; }
}

internal class ReservationTypeConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> modelBuilder)
    {
        modelBuilder.ToTable("Reservations", "dbo");
        modelBuilder.HasKey(e => e.Id);
        modelBuilder.Property(e => e.ReservationDate).IsRequired();
        modelBuilder.Property(e => e.PartySize).IsRequired();
        modelBuilder.HasOne(e => e.Customer).WithMany().HasForeignKey(e => e.CustomerId);
        modelBuilder.HasOne(e => e.Restaurant).WithMany().HasForeignKey(e => e.RestaurantId);
        modelBuilder.HasOne(e => e.Table).WithMany().HasForeignKey(e => e.TableId);
    }
}