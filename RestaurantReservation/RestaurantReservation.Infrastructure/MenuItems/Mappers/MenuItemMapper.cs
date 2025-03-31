using RestaurantReservation.Infrastructure.MenuItems.Models;
using Riok.Mapperly.Abstractions;
using DomainMenuItem = RestaurantReservation.Domain.MenuItems.Models.MenuItem;

namespace RestaurantReservation.Infrastructure.MenuItems.Mappers;

[Mapper]
public static partial class MenuItemMapper
{

    public static partial DomainMenuItem ToDomain(this MenuItem source);
    
    public static partial MenuItem ToEntity(this DomainMenuItem source);
    
    public static partial void UpdateDomainToInfrastructure(this DomainMenuItem source, MenuItem domain);
}