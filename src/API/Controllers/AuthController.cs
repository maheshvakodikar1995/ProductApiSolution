using API.Models;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(
            IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
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
}
