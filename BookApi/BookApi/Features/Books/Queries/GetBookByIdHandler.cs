using BookApi.Data;
using BookApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Features.Books.Queries;

public class GetBookByIdHandler
{
    private readonly AppDbContext _context;

    public GetBookByIdHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Book?> HandleAsync(GetBookByIdQuery query)
    {
        return await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == query.Id);
    }
}