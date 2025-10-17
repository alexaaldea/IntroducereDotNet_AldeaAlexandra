namespace BookApi.Features.Books.Commands;

public record UpdateBookCommand(int Id, string Title, string Author, int Year);