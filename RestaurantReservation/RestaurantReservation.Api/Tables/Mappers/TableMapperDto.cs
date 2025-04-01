using RestaurantReservation.Api.Contracts.Tables.Models;
using Riok.Mapperly.Abstractions;
using DomainTable = RestaurantReservation.Domain.Tables.Models.Table;

namespace RestaurantReservation.Api.Tables.Mappers;

[Mapper]
public static partial class TableMapperDto
{
    public static partial TableResponse ToResponse(this DomainTable source);
    
    public static partial DomainTable ToDomain(this TableRequest source);
}