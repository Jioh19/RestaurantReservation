using RestaurantReservation.Api.Contracts.Employees.Models;
using Riok.Mapperly.Abstractions;
using DomainEmployee = RestaurantReservation.Domain.Employees.Models.Employee;

namespace RestaurantReservation.Api.Employees.Mappers;

[Mapper]
public static partial class EmployeeMapperDto
{
    public static partial EmployeeResponse ToResponse(this DomainEmployee source);
    
    [MapperIgnoreSource(nameof(EmployeeRequest.RestaurantId))]
    [MapperIgnoreTarget(nameof(DomainEmployee.Restaurant))]
    public static partial DomainEmployee ToDomain(this EmployeeRequest source);
}