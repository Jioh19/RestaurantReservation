using RestaurantReservation.Infrastructure.Tables.Models;
using Riok.Mapperly.Abstractions;
using DomainTable = RestaurantReservation.Domain.Tables.Models.Table;

namespace RestaurantReservation.Infrastructure.Tables.Mappers;

[Mapper]
public static partial class TableMapper
{

    [MapperIgnoreSource(nameof(Table.Restaurant))]
    public static partial DomainTable ToDomain(this Table source);
    
    [MapperIgnoreTarget(nameof(Table.Restaurant))]
    public static partial Table ToEntity(this DomainTable source);
    
    [MapperIgnoreTarget(nameof(Table.Restaurant))]
    public static partial void UpdateDomainToInfrastructure(this DomainTable source, Table domain);
}