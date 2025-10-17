using BookApi.Data;
using BookApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Features.Books.Commands;

public class UpdateBookHandler
{
    private readonly AppDbContext _context;

    public UpdateBookHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Book?> HandleAsync(UpdateBookCommand command)
    {
        var book = await _context.Books.FindAsync(command.Id);
        if (book is null) return null;

        book.Title = command.Title;
        book.Author = command.Author;
        book.Year = command.Year;

        await _context.SaveChangesAsync();
        return book;
    }
}