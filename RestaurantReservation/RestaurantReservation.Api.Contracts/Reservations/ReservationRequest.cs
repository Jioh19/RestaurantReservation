using RestaurantReservation.Api.Contracts.EntityReferences;

namespace RestaurantReservation.Api.Contracts.Reservations;

public record ReservationRequest(
    long Id, 
    EntityReference<long> Restaurant,
    EntityReference<long> Customer,
    EntityReference<long> Table,
    DateTime ReservationDate,
    int PartySize
    );