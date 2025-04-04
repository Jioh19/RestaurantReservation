using RestaurantReservation.Infrastructure.MenuItems.Models;
using Riok.Mapperly.Abstractions;
using DomainMenuItem = RestaurantReservation.Domain.MenuItems.Models.MenuItem;

namespace RestaurantReservation.Infrastructure.MenuItems.Mappers;

[Mapper]
public static partial class MenuItemMapper
{

    [MapperIgnoreSource(nameof(MenuItem.Restaurant))]
    public static partial DomainMenuItem ToDomain(this MenuItem source);
    
    [MapperIgnoreTarget(nameof(MenuItem.Restaurant))]
    public static partial MenuItem ToEntity(this DomainMenuItem source);
    
    [MapperIgnoreTarget(nameof(MenuItem.Restaurant))]
    public static partial void UpdateDomainToInfrastructure(this DomainMenuItem source, MenuItem domain);
}