using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Data;
using BookLibraryApi.Models;

namespace BookLibraryApi.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet("{id}")]
        public async Task<ActionResult<BookKeeper>> GetBookKeeper(int id)
        {
            var bookKeeper = await _context.BookKeeper.FindAsync(id);

            if (bookKeeper == null)
            {
                return NotFound();
            }

            return bookKeeper;
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
        public async Task<ActionResult<BookKeeper>> PostBookKeeper(BookKeeper bookKeeper)
        {
            _context.BookKeeper.Add(bookKeeper);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookKeeper", new { id = bookKeeper.BookKeeperId }, bookKeeper);
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
