using RestaurantReservation.Api.Contracts.Restaurants.Models;
using DomainRestaurant = RestaurantReservation.Domain.Restaurants.Models.Restaurant;
using Riok.Mapperly.Abstractions;

namespace RestaurantReservation.Api.Restaurant.Mappers;

[Mapper]
public static partial class RestaurantMapperDto
{
    public static partial RestaurantResponse ToResponse(this DomainRestaurant source);

    public static partial DomainRestaurant ToDomain(this RestaurantRequest source);
}