namespace RestaurantReservation.Api.Jwt;

public class LoginRequest
{
    public long Id { get; set; }
    public string LastName { get; set; } = string.Empty;
}