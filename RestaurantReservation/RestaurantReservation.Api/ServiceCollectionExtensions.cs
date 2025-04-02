using RestaurantReservation.Domain.Customers.Services;
using RestaurantReservation.Domain.Employees.Services;
using RestaurantReservation.Domain.MenuItems.Service;
using RestaurantReservation.Domain.Orders.Services;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Domain.Reservations.Services;
using RestaurantReservation.Domain.Restaurants.Services;
using RestaurantReservation.Domain.Tables.Services;
using RestaurantReservation.Infrastructure.Customers.Repositories;
using RestaurantReservation.Infrastructure.Employees.Repositories;
using RestaurantReservation.Infrastructure.MenuItems.Repositories;
using RestaurantReservation.Infrastructure.Orders.Repositories;
using RestaurantReservation.Infrastructure.Reservations.Repositories;
using RestaurantReservation.Infrastructure.Restaurants.Repositories;
using RestaurantReservation.Infrastructure.Tables.Repositories;

namespace RestaurantReservation.Api;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<ITableService, TableService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IMenuItemService, MenuItemService>();
    }

    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IMenuItemRepository, MenuItemRepository>();
    }
}