using Xunit;
using BookApi.Controllers;
using BookApi.Models;
using BookApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BookApi.Tests
{
    public class BooksControllerTests
    {
        private BookDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new BookDbContext(options);

            context.Books.AddRange(
                new Book { Title = "Test Book 1", Author = "Author 1", PublicationDate = DateTime.Now, Price = 10 },
                new Book { Title = "Test Book 2", Author = "Author 2", PublicationDate = DateTime.Now, Price = 20 }
            );
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsBook()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new BooksController(context);
            var existingBook = context.Books.First();

            // Act
            var result = await controller.GetById(existingBook.Id);

            // Assert
            Assert.NotNull(result.Result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var book = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(existingBook.Id, book.Id);
        }
        [Fact]
        public async Task Create_AddsNewBook()
        {
            var context = GetInMemoryDbContext();
            var controller = new BooksController(context);

            var newBook = new Book
            {
                Title = "New Book",
                Author = "New Author",
                PublicationDate = DateTime.Now,
                Price = 39.99m
            };

            var result = await controller.Create(newBook);

            var createdAt = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdBook = Assert.IsType<Book>(createdAt.Value);

            Assert.Equal("New Book", createdBook.Title);
            Assert.Equal(3, context.Books.Count()); // היו 2, עכשיו 3
        }
        [Fact]
        public async Task Update_ChangesBookData()
        {
            var context = GetInMemoryDbContext();
            var controller = new BooksController(context);

            var existingBook = context.Books.First();
            existingBook.Title = "Updated Title";
            
            var result = await controller.Update(existingBook.Id, existingBook);

            Assert.IsType<NoContentResult>(result);
            Assert.Equal("Updated Title", context.Books.Find(existingBook.Id)!.Title);
        }
        [Fact]
        public async Task Delete_RemovesBook()
        {
            var context = GetInMemoryDbContext();
            var controller = new BooksController(context);

            var book = context.Books.First();
            var result = await controller.Delete(book.Id);

            Assert.IsType<NoContentResult>(result);
            Assert.Null(context.Books.Find(book.Id));
        }


    }
}
