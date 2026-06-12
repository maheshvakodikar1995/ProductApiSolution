using Application.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Products.Queries.GetProductById;

/// <summary>
/// Query to retrieve a single product by identifier.
/// </summary>
/// <param name="Id">Product identifier.</param>
public record GetProductByIdQuery(int Id)
    : IRequest<ProductDto>;

/// <summary>
/// Handles <see cref="GetProductByIdQuery"/> and maps the entity to <see cref="ProductDto"/>.
/// </summary>
public class GetProductByIdQueryHandler
    : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductByIdQueryHandler"/> class.
    /// </summary>
    public GetProductByIdQueryHandler(
        IProductRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<ProductDto> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        var product =
            await _repository.GetByIdAsync(request.Id);

        if (product == null)
            throw new NotFoundException(
                $"Product {request.Id} not found");

        return _mapper.Map<ProductDto>(product);
    }
}
