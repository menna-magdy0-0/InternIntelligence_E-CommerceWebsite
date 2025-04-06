```markdown
# 🛍️ E-Commerce API System

A full-featured E-Commerce RESTful API built with **C# .NET Core** and **Entity Framework Core**. This system handles product, category, cart, and order management with secure **JWT authentication** and **role-based authorization** for customers and admins.

---

## 🚀 Features

- 🧑‍💼 **User Authentication & Authorization**
  - JWT-based login & registration
  - Role-based access control (`Admin`, `Customer`)

- 🛒 **Cart Management**
  - Add, update, remove, and clear cart items
  - Cart auto-creation on first access

- 📦 **Product Management**
  - Admins can create, update, and delete products
  - Users can view all products and filter by category

- 🗂️ **Category Management**
  - Admin-only category creation, update, and deletion

- 📃 **Order Management**
  - Place orders from cart
  - View own orders (Customers)
  - Manage all orders (Admins)

- 🧾 **Payment Mock**
  - Simulated payment flow using a `PaymentRequest` DTO

---

## 📁 Project Structure

```
ECommerceAPI/
├── Controllers/
│   ├── AuthController.cs
│   ├── ProductsController.cs
│   ├── CategoriesController.cs
│   ├── CartController.cs
│   └── OrdersController.cs
├── Models/
│   ├── ApplicationUser.cs
│   ├── Product.cs
│   ├── Category.cs
│   ├── Cart.cs
│   ├── CartItem.cs
│   ├── Order.cs
│   ├── OrderItem.cs
│   └── DTOs/
├── Repositories/
│   ├── Interfaces/
│   └── Implementations/
├── Data/
│   └── ApplicationDbContext.cs
├── Services/
├── Program.cs
└── Startup.cs
```

---

## 🔐 Authentication & Roles

### Roles:
- `Admin`: Has access to full product/category/order management.
- `Customer`: Can browse products, manage their cart, and place orders.

### JWT Authentication:
- Issued at login with claims (`userId`, `email`, `role`)
- Stored on the client and sent via the `Authorization: Bearer <token>` header

---

## 🔧 Technologies Used

- **ASP.NET Core 7**
- **Entity Framework Core**
- **SQL Server**
- **JWT (Json Web Token)**
- **Identity (User Management)**
- **AutoMapper**
- **Swagger (API Documentation)**

---

## 🛠️ Setup & Installation

### 1. Clone the Repository
```bash
git clone https://github.com/your-username/ecommerce-api.git
cd ecommerce-api
```

### 2. Update Database Connection

In `appsettings.json`, update your connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ECommerceDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### 3. Apply Migrations & Seed Database
```bash
dotnet ef database update
```

### 4. Run the API
```bash
dotnet run
```

### 5. Swagger UI
Navigate to:
```
https://localhost:<port>/swagger
```
To explore and test all endpoints.

---

## ✅ Sample Endpoints

### 🔐 Authentication
```http
POST /api/auth/register
POST /api/auth/login
```

### 📦 Products
```http
GET /api/products
GET /api/products/{id}
POST /api/products         (Admin only)
PUT /api/products/{id}     (Admin only)
DELETE /api/products/{id}  (Admin only)
```

### 🗂️ Categories
```http
GET /api/categories
POST /api/categories        (Admin only)
PUT /api/categories/{id}    (Admin only)
DELETE /api/categories/{id} (Admin only)
```

### 🛒 Cart
```http
GET /api/cart
POST /api/cart/add
PUT /api/cart/update/{itemId}
DELETE /api/cart/remove/{itemId}
DELETE /api/cart/clear
```

### 🧾 Orders
```http
GET /api/orders
POST /api/orders/create
```

---

## 🧪 Future Enhancements

- ✅ Refresh token mechanism
- ✅ Pagination and filtering support for products
- 🟡 Product image upload & management
- 🟡 Integration with Stripe/PayPal for real payments
- 🟡 User profile management
- 🟡 Admin dashboard statistics

---

