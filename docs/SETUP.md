# Environment Setup

High-level steps to run Product API locally for development.

---

## Prerequisites

| Tool | Version |
|------|---------|
| [.NET SDK](https://dotnet.microsoft.com/download) | 9.0.x |
| [SQL Server](https://www.microsoft.com/sql-server) | 2019+ (LocalDB, Express, or full) |
| [Git](https://git-scm.com/) | Latest |
| [Docker Desktop](https://www.docker.com/products/docker-desktop/) | Optional (for containerized run) |
| [EF Core CLI](https://learn.microsoft.com/ef/core/cli/dotnet) | Optional (`dotnet tool install --global dotnet-ef`) |

---

## 1. Clone the repository

```bash
git clone <repository-url>
cd ProductApiSolution
```

---

## 2. Configure the database

Update the connection string in `src/API/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=ProductApiDB;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;Encrypt=False;"
}
```

Or override with an environment variable:

```bash
# PowerShell
$env:ConnectionStrings__DefaultConnection = "Server=localhost;Database=ProductApiDB;..."
```

---

## 3. Apply database migrations

From the solution root:

```bash
dotnet ef database update \
  --project src/Infrastructure/Infrastructure.csproj \
  --startup-project src/API/API.csproj
```

This creates the `ProductApiDB` schema (tables: `Products`, `Items`, `RefreshTokens`).

---

## 4. Configure JWT (optional for local dev)

Default JWT settings in `appsettings.json` work out of the box. For custom values:

```json
"Jwt": {
  "SecretKey": "your-secret-key-at-least-32-characters",
  "Issuer": "ProductApi",
  "Audience": "ProductApiUsers",
  "ExpiryMinutes": 30
}
```

---

## 5. Run the API

```bash
dotnet run --project src/API/API.csproj
```

| Profile | URL |
|---------|-----|
| HTTP | http://localhost:5037 |
| HTTPS | https://localhost:7102 |

Open Swagger UI: http://localhost:5037/swagger

---

## 6. Verify the setup

**Health check**

```bash
curl http://localhost:5037/health
```

**Login**

```bash
curl -X POST http://localhost:5037/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -d "{\"userName\":\"admin\",\"password\":\"Admin@123\"}"
```

**Create product** (use token from login)

```bash
curl -X POST http://localhost:5037/api/v1/products \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -d "{\"productName\":\"Test Product\"}"
```

---

## Docker setup (alternative)

Requires Docker Desktop.

```bash
docker compose up --build
```

| Service | URL / port |
|---------|------------|
| API | http://localhost:8080 |
| Swagger | http://localhost:8080/swagger |
| SQL Server | localhost:1433 (sa / `YourStrong@Passw0rd`) |

Apply migrations against the Docker SQL instance:

```bash
dotnet ef database update \
  --project src/Infrastructure/Infrastructure.csproj \
  --startup-project src/API/API.csproj \
  --connection "Server=localhost,1433;Database=ProductApiDB;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;Encrypt=False;"
```

---

## Logging

Serilog writes to:

- **Console** — all environments
- **File** — `src/API/logs/product-api-YYYYMMDD.log` (rolling, 7-day retention)

Log levels are configured under the `Serilog` section in `appsettings.json`. Development overrides are in `appsettings.Development.json`.

---

## Solution structure

```
ProductApiSolution/
├── src/
│   ├── API/              # Web host, controllers, middleware
│   ├── Application/      # CQRS handlers, DTOs, validation
│   ├── Domain/           # Entities and domain exceptions
│   └── Infrastructure/   # EF Core, JWT, repositories, Serilog
├── tests/                # Unit and integration tests
├── docs/                 # Project documentation
├── Dockerfile
├── docker-compose.yml
└── .github/workflows/    # CI pipeline
```

---

## Run tests

```bash
dotnet test
```
