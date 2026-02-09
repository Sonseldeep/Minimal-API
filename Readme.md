# Movie Api 

A clean ASP.NET Core Web API for managing Movies with full CRUD, authentication & authorization, and modern backend best practices. This project is designed as a learning reference.


## 🧱 Architecture Overview

This project follows **Clean & Scalable API architecture** inspired by real production systems.

```
Movie.Api
│
├── Contracts                    
├── Database
├── Endpoints 
├── Entities 
├── Extenstions 
├── Mappings 
├── Middleware        
├── Repositories 
└── Program.cs       
```

---
## 🚀 Features

- Movie CRUD (Create, Read, Update, Delete)
- Search, filter
- Role‑Based Access Control (RBAC)
- JWT authentication (using `user-jwts` for simplicity)
- Global exception handling middleware
- Repository pattern
- Dependency Injection
- DTOs & mappings
- EF Core with migrations
- Automatic database migration on startup
- Cancellation Token support
- Async/Await everywhere
- Dockerized application

---

#### Endpoint Style

- REST‑based
- Resource‑oriented
- Proper HTTP status codes

Examples:
- `GET /api/movies`
- `GET /api/movies/{id}`
- `POST /api/movies`
- `PUT /api/movies/{id}`
- `DELETE /api/movies/{id}`

---
## 🐳 Docker Support

The project includes a **Dockerfile** for containerized deployment.


## 🧩 Design Patterns & Concepts Used

### 1. Repository Pattern


**Why?**
- Separates business logic from data access
- Improves testability
- Makes future database changes easier

```csharp
IMovieRepository → MovieRepository
```

Endpointsnever talk directly to EF Core — they depend on abstractions.

---

### 2. Dependency Injection (DI)

Used everywhere to:
- Reduce tight coupling
- Improve maintainability
- Enable unit testing

```csharp
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
```

---

### 3. DTOs (Data Transfer Objects)

**Why?**
- Prevent exposing database entities directly
- Control request/response shape
- Improve API versioning & security

Examples:
- `CreateMovieDto`
- `UpdateMovieDto`
- `MovieResponseDto`

---

### 4. Mappings

DTO ↔ Entity mappings keep controllers clean and readable.

- Prevents over-posting
- Centralizes transformation logic

---

### 5. EF Core

- Code‑first approach
- Strongly typed queries
- LINQ-based data access

Used for:
- CRUD operations
- Pagination & filtering
- Migrations

---

#### 6. Database Migrations on Startup

On application startup, pending migrations are applied automatically.

**Why?**
- Reduces manual DB steps
- Works well with Docker & CI/CD
- Ensures schema consistency

---

#### 7. Async / Await

All I/O operations are asynchronous:

```csharp
await _context.Movies.ToListAsync();
```

**Benefits:**
- Better scalability
- Non‑blocking threads
- Handles high concurrent traffic

---

#### 8. CancellationToken

Used in all async endpoints.

**Why?**
- Cancels long‑running DB queries
- Frees server resources if client disconnects
- Critical for real‑world production APIs

Example use case:
- Client closes browser
- Request is canceled
- DB query stops immediately

---

## Step :

To run this project locally

```bash
git clone https://github.com/Sonseldeep/Minimal-API
cd Movie.Api
```

To Store dependencies
```bash
dotnet restore
```
To run the project
```bash
dotnet run
```

## To Create User and Generate Tokens

```bash
using ASP.NET Core user-jwts

# Create user ram as Admin
dotnet user-jwts create --name ram --role "Admin"


# Create user sandeep as SuperAdmin
dotnet user-jwts create --name sandeep --role "SuperAdmin"


# Verify created users
dotnet user-jwts list
```
### 👨‍💻 Author

**Sandeep Shrestha**  
Built for learning, interviews, and real‑world practice 🚀
