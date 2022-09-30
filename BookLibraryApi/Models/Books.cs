using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookLibraryApi.Data;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.Models
{
    public class Books
    {
        [Key]
        public int BookID { get; set; }
        [Column(TypeName ="nvarchar(100)")]
        public string BookName { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string BookDescription { get; set; }
        public int BookCount { get; set; }
        public BookType Category { get; set; }
        public KeepType KeepType { get; set; }
        public DateTime InsertedDate { get; set; }
        public string InsertedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }

        public Status Status { get; set; }
    }

    public enum BookType
    {
        Finance,
        Programming,
        Language,
        Story,
        Novels

    }

    public enum KeepType
    {
        Rent,
        Purchase
    }

    public enum Status
    {
        Active,
        Inactive
    }


public static class BooksEndpoints
{
	public static void MapBooksEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Books", async (BookLibraryApiContext db) =>
        {
            return await db.Books.ToListAsync();
        })
        .WithName("GetAllBookss")
        .Produces<List<Books>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Books/{id}", async (int BookID, BookLibraryApiContext db) =>
        {
            return await db.Books.FindAsync(BookID)
                is Books model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetBooksById")
        .Produces<Books>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Books/{id}", async (int BookID, Books books, BookLibraryApiContext db) =>
        {
            var foundModel = await db.Books.FindAsync(BookID);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            //update model properties here

            await db.SaveChangesAsync();

            return Results.NoContent();
        })   
        .WithName("UpdateBooks")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Books/", async (Books books, BookLibraryApiContext db) =>
        {
            db.Books.Add(books);
            await db.SaveChangesAsync();
            return Results.Created($"/Bookss/{books.BookID}", books);
        })
        .WithName("CreateBooks")
        .Produces<Books>(StatusCodes.Status201Created);


        routes.MapDelete("/api/Books/{id}", async (int BookID, BookLibraryApiContext db) =>
        {
            if (await db.Books.FindAsync(BookID) is Books books)
            {
                db.Books.Remove(books);
                await db.SaveChangesAsync();
                return Results.Ok(books);
            }

            return Results.NotFound();
        })
        .WithName("DeleteBooks")
        .Produces<Books>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}}
