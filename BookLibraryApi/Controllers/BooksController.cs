using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Data;
using BookLibraryApi.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookLibraryApiContext _context;

        public BooksController(BookLibraryApiContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Books>>> GetBooks()
        {
            return await _context.Books.Where(x => x.Status == Status.Active).ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet]
        [Route("GetBooks")]
        public async Task<ActionResult<Books>> GetBooks(int bookID)
        {
            var books = await _context.Books.FindAsync(bookID);

            if (books == null)
            {
                return NotFound();
            }

            return books;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooks(int id, Books books)
        {
            if (id != books.BookID)
            {
                return BadRequest();
            }

            _context.Entry(books).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BooksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("CreateNewBook")]
        public async Task<ActionResult<APIResponse>> CreateNewBook(Books books)
        {
            var entity = _context.Books.FirstOrDefault(book => book.BookName == books.BookName);
            if (entity == null)
            {
                if (books.BookImageURL.Contains("http://localhost:5221"))
                {
                    books.BookImageURL = books.BookImageURL.Replace("http://localhost:5221", "");
                }
                _context.Books.Add(books);
                await _context.SaveChangesAsync();
                var response = new APIResponse
                {
                    Response = true,
                    Status = 200,
                    ResponseMessage = "Book Created.",
                    Data = ""
                };
                return CreatedAtAction("CreateNewBook", response);
            }
            else
            {
                var response = new APIResponse
                {
                    Response = false,
                    Status = 200,
                    ResponseMessage = "Book Name exists in database.",
                    Data = ""
                };
                return CreatedAtAction("CreateNewBook", response);
            }
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("UpdateBookDetails")]
        public async Task<ActionResult<APIResponse>> UpdateBookDetails(Books books)
        {
            var entity = _context.Books.FirstOrDefault(book => book.BookID == books.BookID);
            if(entity != null)
            {
                entity.BookName = books.BookName;
                entity.BookDescription = books.BookDescription;
                await _context.SaveChangesAsync();
                var response = new APIResponse
                {
                    Response = true,
                    Status = 200,
                    ResponseMessage = "Book Details Updated",
                    Data = ""
                };
                return CreatedAtAction("UpdateBookDetails", response);
            }
            else
            {
                var response = new APIResponse
                {
                    Response = false,
                    Status = 200,
                    ResponseMessage = "Book not found in databse",
                    Data = ""
                };
                return CreatedAtAction("UpdateBookDetails", response);
            }
            
        }

        // DELETE: api/Books/5
        [HttpGet]
        [Route("UpdateBookStatus")]
        public async Task<ActionResult<APIResponse>> UpdateBookStatus(int BookID)
        {
            var entity = _context.Books.FirstOrDefault(book => book.BookID == BookID);
            if (entity != null)
            {
                entity.Status = Status.Inactive;
                await _context.SaveChangesAsync();
                var response = new APIResponse
                {
                    Response = true,
                    Status = 200,
                    ResponseMessage = "Book Deleted.",
                    Data = ""
                };
                return CreatedAtAction("UpdateBookStatus", response);
            }
            else
            {
                var response = new APIResponse
                {
                    Response = false,
                    Status = 200,
                    ResponseMessage = "Book not found in databse",
                    Data = ""
                };
                return CreatedAtAction("UpdateBookStatus", response);
            }
        }

        private bool BooksExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }
    }
}
