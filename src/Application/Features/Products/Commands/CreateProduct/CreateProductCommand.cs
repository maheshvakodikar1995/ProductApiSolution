using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.CreateProduct;

/// <summary>
/// Command to create a new product.
/// </summary>
/// <param name="ProductName">Display name of the product.</param>
public record CreateProductCommand(
    string ProductName)
    : IRequest<int>;

/// <summary>
/// Handles <see cref="CreateProductCommand"/> by persisting a new product.
/// </summary>
public class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductCommandHandler"/> class.
    /// </summary>
    public CreateProductCommandHandler(
        IProductRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<int> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = new Product
        {
            ProductName = request.ProductName,
            CreatedBy = "System",
            CreatedOn = DateTime.UtcNow
        };

        await _repository.AddAsync(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
