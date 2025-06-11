# Book Management API

A simple and clean RESTful Web API for managing books, built with .NET 8 and Entity Framework Core.

## Features

- Full CRUD operations (Create, Read, Update, Delete)
- Search for books by title or author
- Pagination support for listing books by page
- SQLite database with Entity Framework Core
- API documentation with Swagger UI

## Getting Started

### Prerequisites

- .NET 8 SDK installed

### Running the Application

1. Restore the database (if needed):
    dotnet ef database update

2. Run the application:
    dotnet run


3. Open your browser and navigate to:
    http://localhost:5002/swagger

## API Endpoints

### Books

| Method | Endpoint                      | Description                      |
|--------|-------------------------------|----------------------------------|
| GET    | `/api/books`                  | Get all books with pagination    |
| GET    | `/api/books/{id}`             | Get a book by ID                 |
| POST   | `/api/books`                  | Add a new book                   |
| PUT    | `/api/books/{id}`             | Update an existing book          |
| DELETE | `/api/books/{id}`             | Delete a book by ID              |
| GET    | `/api/books/search?query=...` | Search by title or author        |

## Data Model

```csharp
public class Book
{
 public int Id { get; set; }
 public string Title { get; set; }
 public string Author { get; set; }
 public DateTime PublicationDate { get; set; }
 public decimal Price { get; set; }
}
```

## Testing

Unit tests were implemented using xUnit to ensure the correctness of the main CRUD functionalities of the Book API. The tested operations include:

- **GET /api/books/{id}**: Retrieving a specific book by ID  
- **POST /api/books**: Adding a new book to the collection  
- **PUT /api/books/{id}**: Updating an existing book's information  
- **DELETE /api/books/{id}**: Removing a book by ID  

Each test is executed against an **in-memory database context** to ensure isolation and repeatability.  
Tests confirm both successful operations and correct error handling for edge cases (e.g., non-existing book IDs).

> The test project is located in a separate folder named `BookApi.Tests` inside the solution directory.  
> Make sure to navigate into that folder before running the test command.

To run the tests:
    cd BookApi.Tests
    dotnet test


## Technologies Used

- C#
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger / OpenAPI


##Development Notes
-The database is built and maintained using EF Core migrations.
-All operations are asynchronous for performance and scalability.
-Code is structured for clarity and future extensibility.

##Possible Extensions
-Input validation using DataAnnotations
-Unit testing (xUnit or NUnit)
-Docker support
-Clean architecture or layered design

Author
Yuval Rubin
Computer Science BSc, Ben-Gurion University
GitHub: https://github.com/yuvalrubi