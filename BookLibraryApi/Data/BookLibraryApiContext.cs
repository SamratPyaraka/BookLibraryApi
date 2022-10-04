using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Models;

namespace BookLibraryApi.Data
{
    public class BookLibraryApiContext : DbContext
    {
        public BookLibraryApiContext (DbContextOptions<BookLibraryApiContext> options)
            : base(options)
        {
        }

        public DbSet<BookLibraryApi.Models.Books> Books { get; set; } = default!;

        public DbSet<BookLibraryApi.Models.Users> Users { get; set; }

        public DbSet<BookLibraryApi.Models.BookKeeper> BookKeeper { get; set; }
    }
}
