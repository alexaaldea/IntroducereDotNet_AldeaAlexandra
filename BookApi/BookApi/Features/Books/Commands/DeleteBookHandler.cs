using BookApi.Data;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Features.Books.Commands;

public class DeleteBookHandler
{
    private readonly AppDbContext _context;

    public DeleteBookHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> HandleAsync(DeleteBookCommand command)
    {
        var book = await _context.Books.FindAsync(command.Id);
        if (book is null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }
}