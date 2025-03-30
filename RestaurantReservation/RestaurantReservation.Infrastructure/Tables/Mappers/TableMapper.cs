using RestaurantReservation.Infrastructure.Tables.Models;
using Riok.Mapperly.Abstractions;
using DomainTable = RestaurantReservation.Domain.Tables.Models.Table;

namespace RestaurantReservation.Infrastructure.Tables.Mappers;

[Mapper]
public static partial class TableMapper
{
    public static partial DomainTable ToDomain(this Table source);
    
    public static partial Table ToEntity(this DomainTable source);
    
    public static partial void UpdateDomainToInfrastructure(this DomainTable source, Table domain);
}