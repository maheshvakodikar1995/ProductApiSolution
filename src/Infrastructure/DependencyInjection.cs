using Application.Interfaces;
using Application.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection
            AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString(
                            "DefaultConnection"));
                });

            services.AddScoped<
                IProductRepository,
                ProductRepository>();

            services.AddScoped<
                IUnitOfWork,
                UnitOfWork>();

            services.AddScoped<
                IJwtService,
                JwtService>();

            services.AddScoped<
                ICurrentUserService,
                CurrentUserService>();

            services.AddHttpContextAccessor();

            services.Configure<JwtSettings>(
                configuration.GetSection(
                    JwtSettings.SectionName));

            return services;
        }
    }
}
