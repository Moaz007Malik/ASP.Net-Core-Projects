using Book_Management_System.Context;
using Book_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_System.Endpoints
{
    public static class BookEndpoints
    {
        public static RouteGroupBuilder MapBookEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/api/books");

            group.MapGet("/", async (BookDbContext db) => await db.Books.ToListAsync());
            group.MapGet("/{id}", async (int id, BookDbContext db) =>
                await db.Books.FindAsync(id) is Book book ? Results.Ok(book) : Results.NotFound());
            group.MapPost("/", async (Book book, BookDbContext db) =>
            {
                db.Books.Add(book);
                await db.SaveChangesAsync();
                return Results.Created($"/api/books/{book.Id}", book);
            });
            group.MapPut("/{id}", async (int id, Book updated, BookDbContext db) =>
            {
                var book = await db.Books.FindAsync(id);
                if (book is null) return Results.NotFound();
                book.Title = updated.Title;
                book.Author = updated.Author;
                book.Genre = updated.Genre;
                book.Year = updated.Year;
                await db.SaveChangesAsync();
                return Results.Ok(book);
            });
            group.MapDelete("/{id}", async (int id, BookDbContext db) =>
            {
                var book = await db.Books.FindAsync(id);
                if (book is null) return Results.NotFound();
                db.Books.Remove(book);
                await db.SaveChangesAsync();
                return Results.Ok();
            });

            return group;
        }
    }
}
