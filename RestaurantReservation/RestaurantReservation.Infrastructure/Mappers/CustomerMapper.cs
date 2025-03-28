using System.Data;
using RestaurantReservation.Domain.Models.Customers;
using RestaurantReservation.Infrastructure.Customers.Models;
using Riok.Mapperly.Abstractions;

namespace RestaurantReservation.Infrastructure.Mappers;

[Mapper]
public static partial class CustomerMapper
{
    public static partial DomainCustomer ToDomain(this Customer source);
    public static partial Customer ToEntity(this DomainCustomer source);
}