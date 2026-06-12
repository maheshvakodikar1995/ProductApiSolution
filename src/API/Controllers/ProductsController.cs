using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Queries.GetProductById;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(
            int id)
        {
            var result =
                await _mediator.Send(
                    new GetProductByIdQuery(id));

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
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
}
