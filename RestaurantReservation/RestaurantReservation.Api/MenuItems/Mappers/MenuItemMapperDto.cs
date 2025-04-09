using RestaurantReservation.Api.Contracts.MenuItems;
using Riok.Mapperly.Abstractions;
using DomainMenuItem = RestaurantReservation.Domain.MenuItems.Models.MenuItem;

namespace RestaurantReservation.Api.MenuItems.Mappers;

[Mapper]
public static partial class MenuItemMapperDto
{
    public static partial MenuItemResponse ToResponse(this DomainMenuItem source);
    
    public static partial DomainMenuItem ToDomain(this MenuItemRequest source);
}