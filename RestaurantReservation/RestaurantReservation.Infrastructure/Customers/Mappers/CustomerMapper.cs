using DomainCustomer = RestaurantReservation.Domain.Customers.Models.Customer;
using RestaurantReservation.Infrastructure.Customers.Models;
using Riok.Mapperly.Abstractions;

namespace RestaurantReservation.Infrastructure.Customers.Mappers;

[Mapper]
public static partial class CustomerMapper
{
    public static partial DomainCustomer ToDomain(this Customer source);
    
    public static partial Customer ToEntity(this DomainCustomer source);
    
    public static partial void UpdateDomainToInfrastructure(DomainCustomer source, Customer destination);
}