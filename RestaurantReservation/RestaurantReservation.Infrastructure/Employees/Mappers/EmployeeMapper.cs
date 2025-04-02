using RestaurantReservation.Infrastructure.Employees.Models;
using RestaurantReservation.Infrastructure.Restaurants.Models;
using Riok.Mapperly.Abstractions;
using DomainEmployee = RestaurantReservation.Domain.Employees.Models.Employee;

namespace RestaurantReservation.Infrastructure.Employees.Mappers;

[Mapper]
public static partial class EmployeeMapper
{
    [MapperIgnoreSource(nameof(Employee.RestaurantId))]
    public static partial DomainEmployee ToDomain(this Employee source);
    
    public static partial Employee ToEntity(this DomainEmployee source);
    
    [MapperIgnoreTarget(nameof(Employee.Restaurant))]
    public static partial void UpdateDomainToInfrastructure(DomainEmployee source, Employee domain);
}