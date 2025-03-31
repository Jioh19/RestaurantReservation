using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Domain.Customers.Services;
using RestaurantReservation.Domain.Employees.Services;
using RestaurantReservation.Domain.MenuItems.Service;
using RestaurantReservation.Domain.Orders.Services;
using RestaurantReservation.Domain.Reservations.Services;
using RestaurantReservation.Domain.Restaurants.Services;
using RestaurantReservation.Domain.Tables.Services;
using RestaurantReservation.Infrastructure.Contexts;
using RestaurantReservation.Infrastructure.Customers.Repositories;
using RestaurantReservation.Infrastructure.Employees.Repositories;
using RestaurantReservation.Infrastructure.MenuItems.Repositories;
using RestaurantReservation.Infrastructure.Orders.Repositories;
using RestaurantReservation.Infrastructure.Reservations.Repositories;
using RestaurantReservation.Infrastructure.Restaurants.Repositories;
using RestaurantReservation.Infrastructure.Tables.Repositories;
using TableReservation.Domain.Tables.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<RestaurantReservationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly(typeof(RestaurantReservationDbContext).Assembly)
        ).EnableSensitiveDataLogging()
    );
builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();

// Register repositories and services
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();

builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<ITableService, TableService>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
#pragma warning restore ASP0014

app.Run();
