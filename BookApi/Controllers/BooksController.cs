using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookApi.Data;
using BookApi.Models;

namespace BookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Books")]
    public class BooksController : ControllerBase
    {
        private readonly BookDbContext _context;

        public BooksController(BookDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// List all books with pagination
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and pageSize must be positive.");

            var books = await _context.Books
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(books);
        }

        /// <summary>
        /// Get specific book by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        /// <summary>
        /// Create new book entries
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Book>> Create(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        /// <summary>
        /// Update existing book
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Book updatedBook)
        {
            if (id != updatedBook.Id)
                return BadRequest();

            _context.Entry(updatedBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a book by ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id) =>
            _context.Books.Any(b => b.Id == id);

        /// <summary>
        /// Search for books by title or author
        /// </summary>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Book>>> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query is required.");

            var loweredQuery = query.ToLower();

            var results = await _context.Books
                .Where(b =>
                    b.Title.ToLower().Contains(loweredQuery) ||
                    b.Author.ToLower().Contains(loweredQuery))
                .ToListAsync();

            return Ok(results);
        }


    }
}
