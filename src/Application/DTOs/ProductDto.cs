namespace Application.DTOs;

/// <summary>
/// Data transfer object representing a product returned by the API.
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Gets or sets the unique product identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the product display name.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the UTC timestamp when the product was created.
    /// </summary>
    public DateTime CreatedOn { get; set; }
}
