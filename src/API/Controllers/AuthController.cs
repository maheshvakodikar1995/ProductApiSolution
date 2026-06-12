using API.Models;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Authentication endpoints for obtaining JWT tokens.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="jwtService">Service used to generate JWT access and refresh tokens.</param>
    public AuthController(
        IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    /// <summary>
    /// Authenticates a user and returns JWT access and refresh tokens.
    /// </summary>
    /// <param name="request">Login credentials.</param>
    /// <returns>Token pair on success.</returns>
    /// <response code="200">Returns access and refresh tokens.</response>
    /// <response code="401">Invalid username or password.</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login(
        LoginRequest request)
    {
        if (request.UserName != "admin" ||
            request.Password != "Admin@123")
        {
            return Unauthorized();
        }

        var accessToken =
            _jwtService.GenerateAccessToken(
                request.UserName,
                "Admin");

        var refreshToken =
            _jwtService.GenerateRefreshToken();

        return Ok(new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }
}
