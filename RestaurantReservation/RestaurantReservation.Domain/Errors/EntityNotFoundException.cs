namespace RestaurantReservation.Domain.Errors;

public class EntityNotFoundException<T>(string id) : Exception($"Record {nameof(T)} with id {id} not found");