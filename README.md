# Finance Backend Project

This repository hosts the backend code for a financial application built using ASP.NET Core Web API (.NET 8) and Microsoft SQL Server. The API handles various financial and user operations, with a robust setup including Entity Framework Core, JWT authentication, and Swagger UI for API testing and documentation.

## Project Overview

The backend focuses on key financial entities including Accounts, User Stocks, Comments, and Stocks. It supports CRUD operations, secured access, and complex queries efficiently.

### Key Features

- **User Authentication**: Robust handling of user authentication using JWT.
- **Swagger Integration**: Configured Swagger UI for API testing with support for JWT authentication.
- **Entity Framework Core**: Leveraging Code-First migrations to manage the database schema.
- **Role-Based Access Control**: Using ASP.NET Identity for handling roles and permissions.

## Technologies

- ASP.NET Core Web API (.NET 8)
- Microsoft SQL Server
- Entity Framework Core (EF Core)
- Swagger (Swashbuckle.AspNetCore)
- JWT Authentication

## Getting Started

### Prerequisites

Ensure you have the following installed:
- .NET 8 SDK
- Microsoft SQL Server
- Suitable IDE like Visual Studio or VSCode with C# extensions

### Setup and Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/buenocarlos6971/Finance-BE-Project.git
   cd Finance-BE-Project
   
3. **Restore and update the database**:
   ```bash
   dotnet restore
   dotnet ef database update
   
5. **Run the application via SWAGGER**:
   ```bash
   dotnet watch run

### Testing with Swagger

Access Swagger UI at `http://localhost:5175/swagger` to view and test the available API endpoints. Swagger is configured to allow easy API authentication using JWT:

1. Create a login using the `/api/account/register`
2. Use the `/api/account/login` endpoint to obtain a JWT.
3. Click the 'Authorize' button in Swagger and enter the token prefixed with `Bearer `.

## Configuration

### Program.cs

JWT and Swagger configurations are set up as follows:

- **JWT Configuration**: JWT Bearer is configured to validate tokens based on issuer, audience, and the signing key obtained from app settings.
- **Swagger Configuration**: Includes security definitions to integrate JWT authorization in the Swagger UI, making it easier to test secured endpoints.
