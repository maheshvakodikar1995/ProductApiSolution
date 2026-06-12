using Application.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(int Id)
    : IRequest<ProductDto>;

    public class GetProductByIdQueryHandler
    : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(
            IProductRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

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
}
