using BookApi.Data;
using BookApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Features.Books.Queries;

public class GetAllBooksHandler
{
    private readonly AppDbContext _context;

    public GetAllBooksHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> HandleAsync(GetAllBooksQuery query)
    {
        return await _context.Books
            .AsNoTracking()
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();
    }
}