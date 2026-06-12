using Application.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity;

/// <summary>
/// HMAC-SHA256 JWT token generator configured via <see cref="JwtSettings"/>.
/// </summary>
public class JwtService : IJwtService
{
    private readonly JwtSettings _settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtService"/> class.
    /// </summary>
    /// <param name="options">Bound JWT configuration from appsettings.</param>
    public JwtService(
        IOptions<JwtSettings> options)
    {
        _settings = options.Value;
    }

    /// <inheritdoc />
    public string GenerateAccessToken(
        string userName,
        string role)
    {
        var claims = new[]
        {
            new Claim(
                ClaimTypes.Name,
                userName),

            new Claim(
                ClaimTypes.Role,
                role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _settings.SecretKey));

        var credentials =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

        var token =
            new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    _settings.ExpiryMinutes),
                signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    /// <inheritdoc />
    public string GenerateRefreshToken()
    {
        return Guid.NewGuid()
            .ToString("N");
    }
}
