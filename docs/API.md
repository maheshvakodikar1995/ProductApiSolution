# API Reference

REST API for product management with JWT authentication. All versioned routes use the prefix `/api/v1`.

**Interactive documentation**

| Tool | URL (local) |
|------|-------------|
| Swagger UI | http://localhost:5037/swagger |
| OpenAPI JSON | http://localhost:5037/swagger/v1/swagger.json |
| Health check | http://localhost:5037/health |

**Authentication header** (protected endpoints):

```http
Authorization: Bearer {accessToken}
```

---

## Auth

### POST `/api/v1/auth/login`

Authenticates a user and returns JWT tokens.

| | |
|---|---|
| **Auth required** | No |
| **Content-Type** | `application/json` |

**Request body**

```json
{
  "userName": "admin",
  "password": "Admin@123"
}
```

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `userName` | string | Yes | Login username |
| `password` | string | Yes | Login password |

**Responses**

| Status | Description | Body |
|--------|-------------|------|
| `200 OK` | Login successful | `{ "accessToken": "...", "refreshToken": "..." }` |
| `401 Unauthorized` | Invalid credentials | Empty |

**Example**

```bash
curl -X POST http://localhost:5037/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -d '{"userName":"admin","password":"Admin@123"}'
```

---

## Products

### GET `/api/v1/products/{id}`

Returns a single product by ID.

| | |
|---|---|
| **Auth required** | No |
| **Path parameters** | `id` (integer) — product identifier |

**Responses**

| Status | Description | Body |
|--------|-------------|------|
| `200 OK` | Product found | See `ProductDto` below |
| `404 Not Found` | Product does not exist | `{ "error": "Product {id} not found" }` |

**ProductDto**

```json
{
  "id": 1,
  "productName": "Widget",
  "createdOn": "2026-06-11T15:00:00Z"
}
```

**Example**

```bash
curl http://localhost:5037/api/v1/products/1
```

---

### POST `/api/v1/products`

Creates a new product.

| | |
|---|---|
| **Auth required** | Yes (JWT Bearer) |
| **Content-Type** | `application/json` |

**Request body**

```json
{
  "productName": "Widget"
}
```

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `productName` | string | Yes | Name of the product |

**Responses**

| Status | Description | Body |
|--------|-------------|------|
| `201 Created` | Product created | Product ID (integer) |
| `401 Unauthorized` | Missing or invalid token | Empty |
| `500 Internal Server Error` | Unexpected failure | `{ "error": "..." }` |

**Example**

```bash
curl -X POST http://localhost:5037/api/v1/products \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer {accessToken}" \
  -d '{"productName":"Widget"}'
```

---

## System

### GET `/health`

ASP.NET Core health check endpoint. Returns `200` when the application is running.

### GET `/weatherforecast`

Sample minimal API endpoint (template). Returns a 5-day weather forecast array. No authentication required.

---

## Error format

Unhandled domain exceptions are converted to JSON by `ExceptionMiddleware`:

```json
{
  "error": "Error message"
}
```

| Exception | HTTP status |
|-----------|-------------|
| `NotFoundException` | 404 |
| `ValidationException` | 400 |
| Other | 500 |

---

## API versioning

- Default version: **1.0**
- URL pattern: `/api/v{version}/...`
- Configured in `src/API/Extensions/ApiVersioningExtensions.cs`

---

## OpenAPI / Swagger setup

Swagger is configured in `src/API/Extensions/SwaggerExtensions.cs`:

- Document title: **Product API**
- JWT **Bearer** security scheme on the `Authorization` header
- XML documentation comments from controller and model assemblies are included in the spec

Use **Authorize** in Swagger UI, enter `Bearer {token}` (or paste the token only, depending on UI version), then call protected endpoints.
