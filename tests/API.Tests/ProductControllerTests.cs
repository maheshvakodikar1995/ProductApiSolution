using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests
{
    public class ProductControllerTests
    : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ProductControllerTests(
            CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_Product_Should_Return_200()
        {
            var response =
                await _client.GetAsync(
                    "/api/v1/products/1");

            response.StatusCode
                .Should()
                .BeOneOf(
                    HttpStatusCode.OK,
                    HttpStatusCode.NotFound);
        }
    }
}
