TaskManagement API
Overview
TaskManagement API is a robust .NET 8 Web API backend for a Task Management System. It provides RESTful endpoints for managing tasks and categories, built with a Code First approach using Entity Framework Core. The API features Unit of Work and Repository patterns for data access, Fluent Validation for input validation, and CORS support for integration with the TaskManagement Angular frontend.
The API supports creating, updating, deleting, and filtering tasks (by status: all, completed, pending), as well as bulk deletion for efficient task management. It is designed to be scalable, maintainable, and production-ready.
Features

CRUD Operations:
Create, read, update, and delete tasks.
Manage task categories.


Bulk Deletion: Delete multiple tasks in a single request.
Status Filtering: Retrieve tasks by status (all, completed, pending).
Input Validation: Enforce data integrity with Fluent Validation.
Database: SQL Server with Code First migrations.
Architecture: Unit of Work, Repository Pattern for clean data access.
API Documentation: Swagger UI for endpoint exploration.

Tech Stack

Framework: .NET 8
ORM: Entity Framework Core (Code First)
Database: SQL Server
Validation: Fluent Validation
API Documentation: Swashbuckle (Swagger)
Patterns: Unit of Work, Repository
Tools: Visual Studio 2022, SQL Server Management Studio

Prerequisites

.NET 8 SDK
SQL Server (or SQL Server Express)
Visual Studio 2022 (recommended) or VS Code
Entity Framework Core CLI (dotnet-ef)

Setup Instructions

Clone the Repository:
git clone https://github.com/<your-username>/TaskManagementApi.git
cd TaskManagementApi


Configure the Database:

Update the connection string in appsettings.json:"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TaskManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
}


Ensure SQL Server is running and accessible.

Apply Migrations:

Install the EF Core CLI tool (if not already installed):dotnet tool install --global dotnet-ef


Create and apply migrations:dotnet ef migrations add InitialCreate
dotnet ef database update




Run the API:

Start the application:dotnet run


The API will be available at https://localhost:7139.


Explore Endpoints:

Open Swagger UI at https://localhost:7139/swagger.
Test endpoints like:
POST /api/Tasks: Create a task.
GET /api/Tasks: List all tasks.
GET /api/Tasks/ByStatus/completed: Filter completed tasks.
DELETE /api/Tasks/bulk: Delete multiple tasks.





API Endpoints



Method
Endpoint
Description



POST
/api/Tasks
Create a new task


GET
/api/Tasks
Get all tasks


GET
/api/Tasks/{id}
Get a task by ID


PUT
/api/Tasks/{id}
Update a task


DELETE
/api/Tasks/{id}
Delete a task


DELETE
/api/Tasks/bulk
Delete multiple tasks (body: IDs)


GET
/api/Tasks/ByStatus/{status}
Filter tasks by status (all, completed, pending)


GET
/api/Categories
Get all categories


Example Request
Create a Task:
POST https://localhost:7139/api/Tasks
Content-Type: application/json

{
  "title": "Complete Project",
  "description": "Finish the TaskManagement API",
  "dueDate": "2025-06-01",
  "isCompleted": false,
  "categoryId": 1
}

Response:
{
  "id": 1,
  "title": "Complete Project",
  "description": "Finish the TaskManagement API",
  "dueDate": "2025-06-01",
  "isCompleted": false,
  "categoryId": 1,
  "categoryName": "Work"
}

Contributing

Fork the repository.
Create a feature branch (git checkout -b feature/new-feature).
Commit changes (git commit -m "Add new feature").
Push to the branch (git push origin feature/new-feature).
Open a Pull Request.

License
This project is licensed under the MIT License.
Contact
For questions or feedback, reach out to [your-email@example.com] or open an issue on GitHub.
