using BookLibraryApi.Data;
using BookLibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.Controllers
{
    public static class UsersEndpoints
    {
        public static void MapUsersEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/api/Users", async (BookLibraryApiContext db) =>
            {
                return await db.Users.ToListAsync();
            })
            .WithName("GetAllUserss")
            .Produces<List<Users>>(StatusCodes.Status200OK);

            routes.MapGet("/api/Users/{id}", async (int UserID, BookLibraryApiContext db) =>
            {
                return await db.Users.FindAsync(UserID)
                    is Users model
                        ? Results.Ok(model)
                        : Results.NotFound();
            })
            .WithName("GetUsersById")
            .Produces<Users>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            routes.MapPut("/api/Users/{id}", async (int UserID, Users users, BookLibraryApiContext db) =>
            {
                var foundModel = await db.Users.FindAsync(UserID);

                if (foundModel is null)
                {
                    return Results.NotFound();
                }
                //update model properties here

                await db.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("UpdateUsers")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

            routes.MapPost("/api/Users/", async (Users users, BookLibraryApiContext db) =>
            {
                db.Users.Add(users);
                await db.SaveChangesAsync();
                return Results.Created($"/Userss/{users.UserID}", users);
            })
            .WithName("CreateUsers")
            .Produces<Users>(StatusCodes.Status201Created);


            routes.MapDelete("/api/Users/{id}", async (int UserID, BookLibraryApiContext db) =>
            {
                if (await db.Users.FindAsync(UserID) is Users users)
                {
                    db.Users.Remove(users);
                    await db.SaveChangesAsync();
                    return Results.Ok(users);
                }

                return Results.NotFound();
            })
            .WithName("DeleteUsers")
            .Produces<Users>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}
