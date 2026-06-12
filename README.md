# Product API

RESTful API built with Clean Architecture, CQRS, and JWT authentication.

## Stack

- .NET 9
- Clean Architecture (API, Application, Domain, Infrastructure)
- CQRS + MediatR
- Entity Framework Core + SQL Server
- JWT Bearer authentication
- FluentValidation + AutoMapper
- Serilog (Console + rolling file)
- Swagger / OpenAPI
- Docker + Docker Compose
- GitHub Actions CI

## Documentation

| Document | Description |
|----------|-------------|
| [API Reference](docs/API.md) | All endpoints, request/response schemas, error format |
| [Authentication](docs/AUTHENTICATION.md) | High-level JWT login and token validation flow |
| [Environment Setup](docs/SETUP.md) | Prerequisites, database, local and Docker run |
| [Deployment](docs/DEPLOYMENT.md) | CI, Docker build, production config, verification |

## Quick start

```bash
dotnet ef database update --project src/Infrastructure/Infrastructure.csproj --startup-project src/API/API.csproj
dotnet run --project src/API/API.csproj
```

Open Swagger: http://localhost:5037/swagger

**Demo login:** `admin` / `Admin@123`

## Swagger / OpenAPI

Interactive API docs are available at `/swagger` when the application is running. The OpenAPI spec includes:

- All controller endpoints with XML documentation comments
- JWT Bearer security scheme
- Request and response models

Spec URL: `/swagger/v1/swagger.json`

## Code documentation

Public APIs use **XML documentation comments** (`///`), the C# equivalent of JSDoc. Comments appear in:

- IDE tooltips (IntelliSense)
- Swagger UI (when XML doc generation is enabled)
- Generated `API.xml` / `Application.xml` files at build time

## Project structure

```
src/
├── API/              Controllers, middleware, Swagger, auth extensions
├── Application/      Commands, queries, DTOs, validators
├── Domain/           Entities, domain exceptions
└── Infrastructure/   EF Core, repositories, JWT, Serilog
tests/                API, Application, Infrastructure tests
docs/                 Markdown documentation
```

## License

See repository license file if applicable.
