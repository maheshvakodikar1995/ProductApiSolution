using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Extensions;

/// <summary>
/// Registers Swagger/OpenAPI documentation with JWT security definitions.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Adds Swashbuckle OpenAPI generation with Bearer token support and XML comments.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection
        AddSwaggerDocumentation(
        this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Product API",
                    Version = "v1",
                    Description =
                        "REST API for product management with JWT authentication. " +
                        "See docs/API.md for full reference."
                });

            c.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description =
                        "JWT Authorization header using the Bearer scheme. " +
                        "Example: \"Bearer {token}\"",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

            c.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference =
                                new OpenApiReference
                                {
                                    Type =
                                        ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                        },
                        Array.Empty<string>()
                    }
                });

            IncludeXmlComments(c);
        });

        return services;
    }

    private static void IncludeXmlComments(
        SwaggerGenOptions options)
    {
        foreach (var assembly in new[]
        {
            typeof(Program).Assembly,
            typeof(Application.DTOs.ProductDto).Assembly
        })
        {
            var xmlFile = $"{assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
        }
    }
}
