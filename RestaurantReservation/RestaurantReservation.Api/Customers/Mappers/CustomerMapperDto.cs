using RestaurantReservation.Api.Contracts.Customers.Models;
using DomainCustomer = RestaurantReservation.Domain.Customers.Models.Customer;
using Riok.Mapperly.Abstractions;

namespace RestaurantReservation.Api.Customers.Mappers;

[Mapper]
public static partial class CustomerMapperDto
{
    public static partial CustomerResponse ToResponse(this DomainCustomer source);
    
    
    public static partial DomainCustomer ToDomain(this CustomerRequest source);
}