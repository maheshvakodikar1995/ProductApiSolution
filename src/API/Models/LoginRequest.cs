namespace API.Models;

/// <summary>
/// Request body for the login endpoint.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Gets or sets the login username.
    /// </summary>
    /// <example>admin</example>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the login password.
    /// </summary>
    /// <example>Admin@123</example>
    public string Password { get; set; } = string.Empty;
}
