using RestaurantReservation.Domain.EntityReferences;
using RestaurantReservation.Infrastructure.Restaurants.Models;
using RestaurantReservation.Infrastructure.Tables.Models;
using Riok.Mapperly.Abstractions;
using DomainTable = RestaurantReservation.Domain.Tables.Models.Table;

namespace RestaurantReservation.Infrastructure.Tables.Mappers;

[Mapper]
public static partial class TableMapper
{
    [MapperIgnoreSource(nameof(Table.RestaurantId))]
    public static partial DomainTable ToDomain(this Table source);
    
    [MapProperty(nameof(DomainTable.Restaurant.Id), nameof(Table.RestaurantId))]
    [MapperIgnoreTarget(nameof(Table.Restaurant))]
    public static partial Table ToEntity(this DomainTable source);
    
    [MapperIgnoreTarget(nameof(Table.Restaurant))]
    public static partial void UpdateDomainToInfrastructure(DomainTable source, Table domain);
    
    private static EntityReference<long> ToDomain(Restaurant source) =>
        new() { Id = source.Id, Name = source.Name };
}