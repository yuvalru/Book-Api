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

1. Navigate into the main project folder:
    cd BookApi

2. Apply database migrations (if needed):
    dotnet ef database update

3. Run the application:
    dotnet run

4. Open your browser and navigate to:
    - Swagger UI: http://localhost:5002/swagger
    - Books endpoint: http://localhost:5002/api/books

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

## Development Decisions
This project uses SQLite as the database engine due to its simplicity and ease of setup for small-scale applications and demos. It allows running and testing the application locally without the need for additional database server configuration.
The project structure follows a clean and minimal organization to keep the logic focused and maintainable. Unit tests were implemented using an in-memory database for isolation and repeatability.

## Future Improvements
- Add integration tests for API endpoints
- Add support for Docker-based deployment
- Input validation using DataAnnotations

## Technologies Used
- C#
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger
- xUnit for unit testing

## Development Notes
- The database is built and maintained using EF Core migrations.
- All controller operations are implemented asynchronously.
- Code is structured for clarity and future extensibility.
- Unit tests use an in-memory EF Core database to simulate real behavior
- The API supports searching and pagination for scalability and usability

Author
Yuval Rubin
Computer Science BSc, Ben-Gurion University
GitHub: https://github.com/yuvalru