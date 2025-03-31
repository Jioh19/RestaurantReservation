
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Infrastructure.Restaurants.Models;
using Riok.Mapperly.Abstractions;

namespace RestaurantReservation.Infrastructure.Tables.Models;

public class Table
{
    public long Id { get; set; }
    [MapperIgnore]
    public Restaurant Restaurant { get; set; }
    public long RestaurantId { get; set; }
    public int Capacity { get; set; }
    
}

internal class TableTypeConfiguration : IEntityTypeConfiguration<Table>
{
    public void Configure(EntityTypeBuilder<Table> modelBuilder)
    {
        modelBuilder.ToTable("Tables", "dbo");
        modelBuilder.HasKey(c => c.Id);
        modelBuilder.Property(t => t.Capacity).IsRequired();
        modelBuilder.HasOne(t => t.Restaurant).WithMany().HasForeignKey(t => t.RestaurantId);
    }
}