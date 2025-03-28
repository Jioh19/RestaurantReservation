using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Domain.Models.Customers;
using RestaurantReservation.Domain.Models.Employees;
using RestaurantReservation.Domain.Models.MenuItems;
using RestaurantReservation.Domain.Models.Orders;
using RestaurantReservation.Domain.Models.Reservations;
using RestaurantReservation.Domain.Models.Restaurants;
using RestaurantReservation.Domain.Models.Tables;
using RestaurantReservation.Infrastructure.Customers.Models;

namespace RestaurantReservation.Infrastructure.Contexts;

public class RestaurantReservationDbContext : DbContext
{
    public RestaurantReservationDbContext(DbContextOptions<RestaurantReservationDbContext> options)
        : base(options)
    { }

    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Customer).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
}