
---

```markdown
# 🛒 E-Commerce API System

A robust and scalable E-Commerce backend system built with **C# .NET 8**, **Entity Framework Core**, and **ASP.NET Core Identity**. It provides RESTful API endpoints for managing users, products, categories, carts, and orders, with clean architecture and modular design.

## 🚀 Features

- 🧑‍💼 User Registration & Authentication (JWT)
- 🔒 Role-based Authorization
- 📦 Product & Category Management
- 🛒 Shopping Cart Functionality
- 📑 Order Creation & Tracking
- ⚙️ Repository & Unit of Work Patterns
- 🧪 Validation, Error Handling & API Responses
- 🧵 Asynchronous CRUD Operations
- 🧰 Clean Code Architecture (Separation of Concerns)
- 🧪 Swagger UI for API Testing

## 🧱 Technologies Used

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- ASP.NET Core Web API
- Entity Framework Core
- ASP.NET Core Identity
- JWT Authentication
- AutoMapper
- PostgreSQL
- Swagger / Swashbuckle
- FluentValidation (optional)

---

## 📁 Project Structure

```text
ECommerce.API/
│
├── Controllers/                # API Endpoints
├── Data/                       # ApplicationDbContext & Seed Data
├── Models/                     # Domain Entities
├── Dtos/                       # Data Transfer Objects
├── Interfaces/                 # Repositories Interfaces
├── Repositories/              # Repositories Implementations
├── Services/                  # Business Logic
├── Helpers/                   # Custom Response Wrappers, Pagination, etc.
├── Mappings/                  # AutoMapper Profiles
├── Middlewares/               # Custom Exception Handling Middleware
└── Program.cs / appsettings.json
```

---

## 🔐 Authentication & Authorization

- **JWT-based** authentication
- Secure login and registration with ASP.NET Core Identity
- Role-based access control for Admin, User, etc.

```json
{
  "token": "your-jwt-token",
  "expiration": "2025-12-31T23:59:59"
}
```

---

## 🔄 Sample API Endpoints

```http
POST /api/auth/register
POST /api/auth/login
GET  /api/products
POST /api/products           (Admin only)
GET  /api/categories
POST /api/cart/add
POST /api/orders/create
```

---

## 🔧 Getting Started

### ✅ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- PostgreSQL installed locally or using Docker

### 📦 Installation Steps

1. **Clone the repository**

```bash
git clone https://github.com/your-username/ecommerce-api.git
cd ecommerce-api
```

2. **Update the database connection**

Edit `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=ECommerceDb;Username=postgres;Password=yourpassword"
}
```

3. **Run EF Core Migrations**

```bash
dotnet ef database update
```

4. **Run the application**

```bash
dotnet run
```

The API will be available at: `https://localhost:5001` or `http://localhost:5000`

---

## 📘 API Documentation

Swagger UI is available at:

```bash
https://localhost:5001/swagger
```

Explore and test endpoints directly from your browser.

---

## 🧪 Testing

Use Postman or Swagger to test:

- User Registration/Login
- Adding Products/Categories
- Managing Cart
- Creating Orders

---

## 🙋‍♀️ Author

**Menna Magdy**  
Software Engineer | .NET Backend Developer  
