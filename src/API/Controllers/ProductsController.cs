using Application.DTOs;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Queries.GetProductById;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Product management endpoints (API version 1.0).
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/products")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsController"/> class.
    /// </summary>
    /// <param name="mediator">MediatR sender for CQRS commands and queries.</param>
    public ProductsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets a product by its unique identifier.
    /// </summary>
    /// <param name="id">Product identifier.</param>
    /// <returns>The matching product.</returns>
    /// <response code="200">Returns the product.</response>
    /// <response code="404">Product was not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(
        int id)
    {
        var result =
            await _mediator.Send(
                new GetProductByIdQuery(id));

        return Ok(result);
    }

    /// <summary>
    /// Creates a new product. Requires a valid JWT Bearer token.
    /// </summary>
    /// <param name="command">Product creation payload.</param>
    /// <returns>The identifier of the created product.</returns>
    /// <response code="201">Product created successfully.</response>
    /// <response code="401">Missing or invalid authentication token.</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create(
        CreateProductCommand command)
    {
        var id =
            await _mediator.Send(command);

        return CreatedAtAction(
            nameof(Get),
            new { id },
            id);
    }
}
