using Application.Features.Products.Commands.CreateProduct;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Features
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _repository;

        private readonly Mock<IUnitOfWork> _unitOfWork;

        public CreateProductCommandHandlerTests()
        {
            _repository =
                new Mock<IProductRepository>();

            _unitOfWork =
                new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Handle_Should_Create_Product()
        {
            // Arrange

            var command =
                new CreateProductCommand(
                    "Laptop");

            var handler =
                new CreateProductCommandHandler(
                    _repository.Object,
                    _unitOfWork.Object);

            // Act

            var result =
                await handler.Handle(
                    command,
                    CancellationToken.None);

            // Assert

            _repository.Verify(
                x => x.AddAsync(
                    It.IsAny<Product>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            _unitOfWork.Verify(
                x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
