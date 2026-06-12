using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        [Fact]
        public async Task GetById_Should_Return_Product()
        {
            var options =
                new DbContextOptionsBuilder<
                    ApplicationDbContext>()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString())
                .Options;

            await using var context =
                new ApplicationDbContext(options);

            context.Products.Add(
                new Product
                {
                    ProductName = "Laptop",
                    CreatedBy = "Test",
                    CreatedOn = DateTime.UtcNow
                });

            await context.SaveChangesAsync();

            var repository =
                new ProductRepository(context);

            var result =
                await repository.GetByIdAsync(1);

            result.Should().NotBeNull();

            result!.ProductName
                .Should()
                .Be("Laptop");
        }
    }
}
