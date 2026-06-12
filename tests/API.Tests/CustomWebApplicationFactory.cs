using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests
{
    public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(
            IWebHostBuilder builder)
        {
            builder.ConfigureServices(
                services =>
                {
                    var descriptor =
                        services.SingleOrDefault(
                            d =>
                                d.ServiceType ==
                                typeof(DbContextOptions<ApplicationDbContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<ApplicationDbContext>(
                        options =>
                        {
                            options.UseInMemoryDatabase(
                                "TestDb");
                        });
                });
        }
    }
}
