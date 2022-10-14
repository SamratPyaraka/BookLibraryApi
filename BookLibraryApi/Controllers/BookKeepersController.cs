using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Data;
using BookLibraryApi.Models;
using System.Net;

namespace BookLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BookKeepersController : ControllerBase
    {
        private readonly BookLibraryApiContext _context;

        public BookKeepersController(BookLibraryApiContext context)
        {
            _context = context;
        }

        // GET: api/BookKeepers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookKeeper>>> GetBookKeeper()
        {
            return await _context.BookKeeper.ToListAsync();
        }

        // GET: api/BookKeepers/5
        [HttpGet]
        [Route("GetBooksByUserID")]
        public async Task<ActionResult<List<BookRecords>>> GetBookKeeper(int userID)
        {
            var bookKeeper = await _context.BookKeeper.Where(x => x.UserID == userID).ToListAsync();
            List<BookRecords> bookList = new List<BookRecords>();
            foreach (var bookForKeep in bookKeeper)
            {
                var book = _context.Books.FirstOrDefault(e => e.BookID == bookForKeep.BookID);
                if (book != null)
                {
                    var bookRecord = new BookRecords
                    {
                        BookID = bookForKeep.BookID,
                        ISBN = book.ISBN13,
                        Title = book.Title,
                        HasExpiry = bookForKeep.KeepType == 0 ? true : false,
                        ValidTill = bookForKeep.Expiry,
                        PurchasedOn = bookForKeep.InsertedDate,
                        Amount = bookForKeep.Amount,
                        OwnBorrow = bookForKeep.KeepType == 0 ? "Bowwored" : "Owned"
                    };
                    bookList.Add(bookRecord);
                }
            }

            if (bookKeeper == null)
            {
                return NotFound();
            }

            return bookList;
        }

        // PUT: api/BookKeepers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookKeeper(int id, BookKeeper bookKeeper)
        {
            if (id != bookKeeper.BookKeeperId)
            {
                return BadRequest();
            }

            _context.Entry(bookKeeper).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookKeeperExists(id))
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

        // POST: api/BookKeepers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<APIResponse>> PostBookKeeper(OrderDetails orderDetails)
        {
            var userExists = _context.Users.Any(x => x.UserID == orderDetails.UserID);
            if (userExists)
            {
                DateTime dt = orderDetails.Expiry.UtcDateTime;
                BookKeeper bookKeeper = new BookKeeper
                {
                    UserID = orderDetails.UserID,
                    BookID = orderDetails.BookID,
                    Amount = orderDetails.Amount,
                    KeepType = orderDetails.KeepType,
                    Expiry = dt
                };
                _context.BookKeeper.Add(bookKeeper);
                await _context.SaveChangesAsync();

                var response = new APIResponse
                {
                    Response = true,
                    Status = 200,
                    ResponseMessage = "You have registered successfully.",
                    Data = null
                };

                return CreatedAtAction("GetBookKeeper", new { id = bookKeeper.BookKeeperId }, response);
            }
            else
            {
                var response = new APIResponse
                {
                    Response = true,
                    Status = 200,
                    ResponseMessage = "User Does not exits",
                    Data = null
                };

                return CreatedAtAction("GetBookKeeper",response);
            }
        }

        // DELETE: api/BookKeepers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBookKeeper(int id)
        //{
        //    var bookKeeper = await _context.BookKeeper.FindAsync(id);
        //    if (bookKeeper == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.BookKeeper.Remove(bookKeeper);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool BookKeeperExists(int id)
        {
            return _context.BookKeeper.Any(e => e.BookKeeperId == id);
        }
    }
}
