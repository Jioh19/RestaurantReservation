using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Domain.Employees.Services;

namespace RestaurantReservation.Api.Jwt;

[ApiController]
[Route("api/auth")]
public class AuthController :ControllerBase
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IEmployeeService _userService;

    public AuthController(
        IJwtGenerator jwtGenerator,
        IEmployeeService userService)
    {
        _jwtGenerator = jwtGenerator;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {

        var user = await _userService.ValidateCredentials(
            request.Id, request.LastName);


        if (user is null)
        {
            return Unauthorized();
        }

        // Generate JWT token
        var token = _jwtGenerator.GenerateToken(user);

        return Ok(new { token });
    }
}