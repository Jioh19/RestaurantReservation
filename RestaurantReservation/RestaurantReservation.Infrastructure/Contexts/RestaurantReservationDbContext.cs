using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Domain.Models.Customers;
using RestaurantReservation.Domain.Models.Employees;
using RestaurantReservation.Domain.Models.MenuItems;
using RestaurantReservation.Domain.Models.Orders;
using RestaurantReservation.Domain.Models.Reservations;
using RestaurantReservation.Domain.Models.Restaurants;
using RestaurantReservation.Domain.Models.Tables;

namespace RestaurantReservation.Infrastructure.Contexts;

public class RestaurantReservationDbContext : DbContext
{
    public RestaurantReservationDbContext(DbContextOptions<RestaurantReservationDbContext> options)
        : base(options)
    {
    }

    // DbSets for each entity
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    // Optional: Configure relationships and constraints
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Reservation relationships with explicit delete behavior
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Table)
            .WithMany()
            .HasForeignKey(r => r.TableId)
            .OnDelete(DeleteBehavior.Restrict); // Use Restrict instead of Cascade

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Customer)
            .WithMany()
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Restaurant)
            .WithMany()
            .HasForeignKey(r => r.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure decimal precision for MenuItem
        modelBuilder.Entity<MenuItem>()
            .Property(m => m.Price)
            .HasPrecision(18, 2);

        // Configure decimal precision for Order
        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasPrecision(18, 2);

        // Similar configurations for other entities
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Reservation)
            .WithMany()
            .HasForeignKey(o => o.ReservationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MenuItem>()
            .HasOne(m => m.Restaurant)
            .WithMany()
            .HasForeignKey(m => m.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
}