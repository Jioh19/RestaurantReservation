using DomainRestaurant = RestaurantReservation.Domain.Restaurants.Models.Restaurant;
using RestaurantReservation.Infrastructure.Restaurants.Models;
using Riok.Mapperly.Abstractions;

namespace RestaurantReservation.Infrastructure.Restaurants.Mappers;

[Mapper]
public static partial class RestaurantMapper
{
    public static partial DomainRestaurant ToDomain(this Restaurant source);
    
    public static partial Restaurant ToEntity(this DomainRestaurant source);
    
    public static partial void UpdateDomainToInfrastructure(this DomainRestaurant source, Restaurant target);
}