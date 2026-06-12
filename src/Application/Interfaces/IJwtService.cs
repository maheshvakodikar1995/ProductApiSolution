namespace Application.Interfaces;

/// <summary>
/// Generates JWT access tokens and opaque refresh tokens.
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// Creates a signed JWT access token for the given user and role.
    /// </summary>
    /// <param name="userName">Username embedded in the Name claim.</param>
    /// <param name="role">Role embedded in the Role claim.</param>
    /// <returns>Encoded JWT string.</returns>
    string GenerateAccessToken(
        string userName,
        string role);

    /// <summary>
    /// Creates a new opaque refresh token identifier.
    /// </summary>
    /// <returns>Refresh token string.</returns>
    string GenerateRefreshToken();
}
