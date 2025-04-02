using RestaurantReservation.Domain.Employees.Models;

namespace RestaurantReservation.Api.Jwt;

public interface IJwtGenerator
{
    string GenerateToken(Employee user);
}