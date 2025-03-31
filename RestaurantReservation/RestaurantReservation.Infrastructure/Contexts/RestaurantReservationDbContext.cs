using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Infrastructure.Customers.Models;
using RestaurantReservation.Infrastructure.Restaurants.Models;
using RestaurantReservation.Infrastructure.Tables.Models;
using RestaurantReservation.Infrastructure.Employees.Models;
using RestaurantReservation.Infrastructure.Reservations.Models;

namespace RestaurantReservation.Infrastructure.Contexts;

public class RestaurantReservationDbContext : DbContext
{
    public RestaurantReservationDbContext(DbContextOptions<RestaurantReservationDbContext> options)
        : base(options)
    { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Customer).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Restaurant).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Table).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Employee).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Reservation).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
}