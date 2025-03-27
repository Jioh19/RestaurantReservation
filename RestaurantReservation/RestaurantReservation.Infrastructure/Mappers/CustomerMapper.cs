using RestaurantReservation.Domain.Models.Customers;
using DomainCustomer = RestaurantReservation.Domain.Models.Customers.Customer;
using Riok.Mapperly.Abstractions;

namespace RestaurantReservation.Infrastructure.Mappers;

[Mapper]
public static partial class CustomerMapper
{
    public static partial DomainCustomer ToDomain(this Customer source);
}