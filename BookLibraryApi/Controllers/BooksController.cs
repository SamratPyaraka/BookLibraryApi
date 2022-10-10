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
using System.Formats.Asn1;
using System.Globalization;
using CsvHelper;

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

        [HttpGet]
        [Route("GetLimitedBooks")]
        public async Task<ActionResult<IEnumerable<Books>>> GetBooks(int? skip, int? take)
        {
            var result = _context.Books.AsQueryable();
            result = _context.Books.Where(x => x.Status == Status.Active);
            if (skip != null)
            {
                result = result.Skip((int)skip);
            }

            if (take != null)
            {
                result = result.Take((int)take);
            }
            return await result.ToListAsync();
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
            var entity = _context.Books.FirstOrDefault(book => book.Title == books.Title);
            if (entity == null)
            {
                if (books.ImageURL != null && books.ImageURL.Contains("http://localhost:5221"))
                {
                    books.ImageURL = books.ImageURL.Replace("http://localhost:5221", "");
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
            if (entity != null)
            {
                entity.Title = books.Title;
                entity.Description = books.Description;
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

        [HttpPost]
        [Route("UploadCSVData")]
        public async Task<APIResponse> UploadData(IFormFile file)
        {
            try
            {
                var fileextension = Path.GetExtension(file.FileName);
                var filename = Guid.NewGuid().ToString() + fileextension;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", filename);
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    file.CopyTo(fs);
                }
                if (fileextension == ".csv")
                {
                    using (var reader = new StreamReader(filepath))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<Books>();
                        foreach (var record in records)
                        {

                            if (string.IsNullOrWhiteSpace(record.Title))
                            {
                                break;
                            }
                            Books book = _context.Books.Where(s => s.Title == record.Title).FirstOrDefault();

                            if (book == null)
                            {
                                book = new Books();
                            }

                            book.Title = record.Title;
                            book.Subtitle = record.Subtitle;
                            if(record.Description!=null && record.Description.Length > 3998)
                                book.Description = record.Description.Substring(0,3998);
                            else
                                book.Description = record.Description;
                            book.Authors = record.Authors;
                            book.Category = record.Category;
                            book.AverageRating = record.AverageRating;
                            book.BookCount = record.BookCount;
                            book.ISBN10 = record.ISBN10;
                            book.ISBN13 = record.ISBN13;
                            book.ImageURL = record.ImageURL;
                            book.Status = record.Status;
                            book.InsertedDate = record.InsertedDate;
                            book.InsertedBy = record.InsertedBy;
                            book.LastUpdatedBy = record.LastUpdatedBy;
                            book.LastUpdatedDate = record.LastUpdatedDate;
                            book.NumberOfPages = record.NumberOfPages;
                            book.RatingsCount = record.RatingsCount;

                            if (book.BookID == 0)
                                _context.Books.Add(book);
                            else
                                _context.Books.Update(book);
                        }
                        _context.SaveChanges();
                    }
                }
                else
                {
                    return new APIResponse
                    {
                        Response = false,
                        ResponseMessage = "You can only add CSV file",
                        Status = (int)System.Net.HttpStatusCode.UnprocessableEntity
                    };
                }


            }
            catch (Exception e)
            {
            }

            return new APIResponse
            {
                Response = true,
                ResponseMessage = "Data Updated Successfully",
                Status = (int)System.Net.HttpStatusCode.OK
            };
        }
    }
}
