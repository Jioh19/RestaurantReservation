using RestaurantReservation.Api.Contracts.Customers.Models;
using DomainCustomer = RestaurantReservation.Domain.Customers.Models.Customer;
using Riok.Mapperly.Abstractions;

namespace RestaurantReservation.Api.Customers.Mappers;

[Mapper]
public static partial class CustomerMapperDto
{
    public static partial CustomerResponse ToResponse(this DomainCustomer source);
    
    // Mapping from Create Request to Domain Customer
    public static partial DomainCustomer ToDomain(this CustomerRequest source);
}