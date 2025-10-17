namespace BookApi.Features.Books.Queries;

public record GetAllBooksQuery(int Page = 1, int PageSize = 10);