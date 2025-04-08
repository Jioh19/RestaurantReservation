using RestaurantReservation.Domain.EntityReferences;
using RestaurantReservation.Infrastructure.MenuItems.Models;
using RestaurantReservation.Infrastructure.Restaurants.Models;
using Riok.Mapperly.Abstractions;
using DomainMenuItem = RestaurantReservation.Domain.MenuItems.Models.MenuItem;

namespace RestaurantReservation.Infrastructure.MenuItems.Mappers;

[Mapper]
public static partial class MenuItemMapper
{

    [MapperIgnoreSource(nameof(MenuItem.RestaurantId))]
    public static partial DomainMenuItem ToDomain(this MenuItem source);
    
    [MapProperty(nameof(DomainMenuItem.Restaurant.Id), nameof(MenuItem.RestaurantId))]
    [MapperIgnoreTarget(nameof(MenuItem.Restaurant))]
    public static partial MenuItem ToEntity(this DomainMenuItem source);
    
    [MapperIgnoreTarget(nameof(MenuItem.Restaurant))]
    public static partial void UpdateDomainToInfrastructure(this DomainMenuItem source, MenuItem domain);
    
    private static EntityReference<long> ToDomain(Restaurant source) =>
        new() { Id = source.Id, Name = source.Name };
}