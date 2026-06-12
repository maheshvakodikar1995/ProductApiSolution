using Application.DTOs;
using Application.Features.Products.Queries.GetProductById;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Features
{
    public class GetProductByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_Product()
        {
            var repository =
                new Mock<IProductRepository>();

            var mapper =
                new Mock<IMapper>();

            repository.Setup(x =>
                    x.GetByIdAsync(
                        1,
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Product
                {
                    Id = 1,
                    ProductName = "Laptop"
                });

            mapper.Setup(x =>
                    x.Map<ProductDto>(
                        It.IsAny<Product>()))
                .Returns(new ProductDto
                {
                    Id = 1,
                    ProductName = "Laptop"
                });

            var handler =
                new GetProductByIdQueryHandler(
                    repository.Object,
                    mapper.Object);

            var result =
                await handler.Handle(
                    new GetProductByIdQuery(1),
                    CancellationToken.None);

            result.ProductName
                .Should()
                .Be("Laptop");
        }
    }
}
