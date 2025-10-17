namespace BookApi.Features.Books.Commands;

public record CreateBookCommand(string Title, string Author, int Year);