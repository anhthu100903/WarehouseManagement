# Warehouse Management API

Warehouse Management API is a backend system built with ASP.NET Core Web API (.NET 8) for managing warehouse operations in a scalable and structured manner.

The project is designed with a clean layered architecture, focusing on maintainability, separation of concerns, and enterprise-ready backend practices.

---

## 🎯Project Purpose
- Build a structured warehouse management backend system

- Apply Clean Architecture principles

- Implement secure authentication and role-based authorization

- Ensure scalability and maintainability for real-world scenarios

---

## 🏗 Architecture
The project follows a layered Clean Architecture structure:
```bash
WarehouseManagement
 ├── Domain          # Entities and core business rules
 ├── Application     # Interfaces and business logic
 ├── Infrastructure  # EF Core, DbContext, repository implementations
 └── Api             # Controllers, Middleware, Authentication
```
## Layer Responsibilities
- Domain: Core entities and domain rules
- Application: Business services and abstractions
- Infrastructure: Database access using EF Core
- API: HTTP endpoints, authentication, middleware pipeline

This structure ensures clear separation between business logic and infrastructure concerns.

### ✨ Key Features
- JWT Authentication
- Role-based Authorization (Owner, Manager, Staff)
- Store-level access control middleware
- Soft Delete using Global Query Filters
- Concurrency handling with RowVersion
- Dependency Injection
- Asynchronous programming (async/await)
- RESTful API design

## 🛠 Tech Stack
- **Framework:** .NET 8.0 (ASP.NET Core Web API)
- **Database:** SQL Server
- **ORM:** Entity Framework Core 8.0.12
- **Authentication:** JWT Bearer
-**Password Hashing:** BCrypt
-**Version Control:** Git & GitHub
- **IDE:** Visual Studio 2022

## 📦 NuGet Packages
Dự án sử dụng các thư viện thuộc hệ sinh thái .NET 8 để đảm bảo tính ổn định và bảo mật:

🗄️ Database & ORM
- Microsoft.EntityFrameworkCore.SqlServer (8.0.12)
- Microsoft.EntityFrameworkCore.Tools (8.0.12)
- Microsoft.EntityFrameworkCore.Design (8.0.12)

## 🔐 Security & Authentication
- BCrypt.Net-Next
- Microsoft.AspNetCore.Authentication.JwtBearer
  
### 🚀 Getting Started
### 1️⃣Clone repository

```bash
git clone https://github.com/anhthu100903/WarehouseManagement.git
```

### 2. Configure Database
Update the connection string in:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=WarehouseDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```
### 3️⃣ Apply Migrations
```bash
Add-Migration InitialCreate
Update-Database
```

### 4️⃣ Run the application
```bash
dotnet run
```

Access Swagger UI:
[https://localhost:<port>/swagger](https://localhost:<port>/swagger)

### 🧠 Concurrency Handling
The project uses RowVersion to prevent data conflicts when multiple users update the same record simultaneously.
This ensures data consistency in concurrent environments.

### 📈 Future Improvements
Add Unit Testing (xUnit + Moq)
Implement Docker support
Add logging (Serilog)
Introduce caching for performance optimization

👤 Author
GitHub: anhthu100903
