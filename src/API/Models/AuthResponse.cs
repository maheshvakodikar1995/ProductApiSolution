namespace API.Models;

/// <summary>
/// Response returned after a successful login.
/// </summary>
public class AuthResponse
{
    /// <summary>
    /// Gets or sets the JWT access token used on protected API requests.
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the refresh token (not yet exchangeable via API).
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}
