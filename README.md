# Product & Sales Management System

Full-stack application for managing products, clients, and sales with JWT authentication.

## Technologies

### Backend
- .NET 9.0
- C# 
- SQL Server
- Entity Framework Core
- JWT Authentication
- BCrypt for password hashing

### Frontend
- Vue.js 3 (Composition API)
- Axios
- Vue Router

## Features

- JWT-based authentication
- Product CRUD operations
- Client CRUD operations
- Sales management
- Responsive design
- RESTful API architecture

## Architecture

- **Layered Architecture**: Controllers → Services → DbContext
- **SOLID Principles**: Dependency injection, single responsibility
- **Repository Pattern**: Simplified for development speed

## Setup Instructions

### Backend

1. Update connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ProductSalesDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

2. Run migrations:
```bash
dotnet ef database update
```

3. Start the API:
```bash
dotnet run
```

API will be available at: `http://localhost:5119`

### Frontend

1. Navigate to frontend folder:
```bash
cd product-sales-frontend
```

2. Install dependencies:
```bash
npm install
```

3. Start development server:
```bash
npm run dev
```

Frontend will be available at: `http://localhost:5173`

### Default User

Create a user via the register endpoint or use Postman:
- POST `http://localhost:5119/api/auth/register`
- Body: `{"username":"admin","password":"admin123"}`

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and get JWT token

### Products (Protected)
- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create product
- `PUT /api/products/{id}` - Update product
- `DELETE /api/products/{id}` - Delete product

### Clients (Protected)
- `GET /api/clients` - Get all clients
- `GET /api/clients/{id}` - Get client by ID
- `POST /api/clients` - Create client
- `PUT /api/clients/{id}` - Update client
- `DELETE /api/clients/{id}` - Delete client

### Sales (Protected)
- `GET /api/sales` - Get all sales
- `GET /api/sales/{id}` - Get sale by ID
- `POST /api/sales` - Create sale

## Project Structure

### Backend
```
ProductSalesAPI/
├── Controllers/     # API endpoints
├── Services/        # Business logic
├── Data/            # DbContext
├── Models/          # Entity models
└── Tests/           # Unit tests
```

### Frontend
```
product-sales-frontend/
├── src/
│   ├── views/       # Page components
│   ├── services/    # API integration
│   └── router/      # Route configuration
```

## Testing

Run backend unit tests:
```bash
dotnet test
```

## Development Notes

- HTTPS redirection disabled in development for easier testing
- CORS enabled for local development
- JWT tokens expire after 3 hours

## Author

Harold Toribio