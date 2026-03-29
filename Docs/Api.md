# SportGym API

Documentation for the current REST API implementation of the SportGym project.

## Base URL

- HTTP (Development): `http://localhost:5137`
- HTTPS (Development): `https://localhost:7205`

> Defined in `SportGym.Api/Properties/launchSettings.json`.

## Prerequisites

Before testing authenticated endpoints, configure a JWT secret in:

- `SportGym.Api/appsettings.Development.json`

Example:

```json
"JwtSettings": {
	"Secret": "your-long-secure-secret",
	"ExpiryMinutes": 60,
	"Issuer": "SportGym",
	"Audience": "SportGym"
}
```

If `Secret` is empty, token signing will fail.

## Authentication and Authorization

- The authentication scheme is **Bearer JWT**.
- All endpoints under controllers inheriting from `ApiController` require authentication by default (`[Authorize]`).
- `AuthenticationController` allows anonymous access (`[AllowAnonymous]`).

Header for protected endpoints:

```http
Authorization: Bearer <jwt_token>
```

## Response Conventions

### Success

- Successful responses: `200 OK`.

### Error (Problem Details)

The API uses `ProblemDetails` (`application/problem+json`) for errors.

Common fields:

- `title`: error description.
- `status`: HTTP status code.
- `traceId`: request trace id.
- `errorCodes`: list of domain error codes (when applicable).

### Validation Error

For input validation failures (FluentValidation), the API returns `400 Bad Request` with `ValidationProblemDetails`, including `errors`.

## Endpoints

## 1. Register

- **Method:** `POST`
- **Route:** `/api/authentication/register`
- **Auth:** No

### Request Body

```json
{
	"firstName": "Diego",
	"lastName": "Gomez",
	"email": "diego.gomez@example.com",
	"password": "123456"
}
```

### Validations

- `firstName` is required.
- `lastName` is required.
- `email` is required.
- `password` is required.

### 200 Response

```json
{
	"id": "9a621296-2f67-4f6d-b96f-00a4ce2cbcb8",
	"firstName": "Diego",
	"lastName": "Gomez",
	"email": "diego.gomez@example.com",
	"token": "<jwt_token>"
}
```

### Common Errors

- `400 Bad Request`: validation error.
- `409 Conflict`: duplicate email (`User.DuplicateEmail`).

## 2. Login

- **Method:** `POST`
- **Route:** `/api/authentication/login`
- **Auth:** No

### Request Body

```json
{
	"email": "diego.gomez@example.com",
	"password": "123456"
}
```

### Validations

- `email` is required.
- `password` is required.

### 200 Response

```json
{
	"id": "9a621296-2f67-4f6d-b96f-00a4ce2cbcb8",
	"firstName": "Diego",
	"lastName": "Gomez",
	"email": "diego.gomez@example.com",
	"token": "<jwt_token>"
}
```

### Common Errors

- `400 Bad Request`: validation error.
- `401 Unauthorized`: invalid credentials.

`401` example:

```json
{
	"type": "https://tools.ietf.org/html/rfc9110#section-15.5.2",
	"title": "The provided credentials are invalid.",
	"status": 401,
	"traceId": "00-..."
}
```

## 3. List Users

- **Method:** `GET`
- **Route:** `/api/user`
- **Auth:** Yes (Bearer JWT)

### Headers

```http
Authorization: Bearer <jwt_token>
```

### 200 Response

```json
[]
```

> It currently returns an empty array (`Array.Empty<string>()`) as a placeholder.

### Common Errors

- `401 Unauthorized`: missing/invalid/expired token.

## OpenAPI

In Development, the application exposes OpenAPI via `app.MapOpenApi()`.

Default route:

- `/openapi/v1.json`

Example:

- `http://localhost:5137/openapi/v1.json`

## Example Request Files (.http)

You can use these files included in the repository:

- `Requests/Authentication/Register.http`
- `Requests/Authentication/Login.http`
- `Requests/Users/ListUsers.http`

## Implementation Notes

- Unhandled errors are processed by `GlobalExceptionHandler` and return `500` with a generic message.
- The JWT token includes these claims: `sub`, `given_name`, `family_name`, `jti`.
- Command/query validations run through a MediatR + FluentValidation pipeline.
