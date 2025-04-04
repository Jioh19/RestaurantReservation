using RestaurantReservation.Domain.EntityReferences;
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
    
    [MapProperty(nameof(DomainEmployee.Restaurant.Id), nameof(Employee.RestaurantId))]
    [MapperIgnoreTarget(nameof(Employee.Restaurant))]
    public static partial Employee ToEntity(this DomainEmployee source);
    
    [MapperIgnoreTarget(nameof(Employee.Restaurant))]
    public static partial void UpdateDomainToInfrastructure(DomainEmployee source, Employee domain);

    private static EntityReference<long> ToDomain(Restaurant source) =>
        new() { Id = source.Id, Name = source.Name };
}