using RestaurantReservation.Infrastructure.Employees.Models;
using Riok.Mapperly.Abstractions;
using DomainEmployee = RestaurantReservation.Domain.Employees.Models.Employee;

namespace RestaurantReservation.Infrastructure.Employees.Mappers;

[Mapper]
public static partial class EmployeeMapper
{
    
    public static partial DomainEmployee ToDomain(this Employee source);
    
    public static partial Employee ToEntity(this DomainEmployee source);
    
    public static partial void UpdateDomainToInfrastructure(this DomainEmployee source, Employee domain);
}