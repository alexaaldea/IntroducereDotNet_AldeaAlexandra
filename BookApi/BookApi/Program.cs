using BookApi.Data;
using BookApi.Features.Books.Commands;
using BookApi.Features.Books.Queries;
using BookApi.Features.Books.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<CreateBookCommandValidator>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();


app.MapPost("/books", async (CreateBookCommand command, AppDbContext db, IValidator<CreateBookCommand> validator) =>
{
    var validation = await validator.ValidateAsync(command);
    if (!validation.IsValid) return Results.BadRequest(validation.Errors);

    var handler = new CreateBookHandler(db);
    var book = await handler.HandleAsync(command);
    return Results.Created($"/books/{book.Id}", book);
});

app.MapPut("/books/{id:int}", async (int id, UpdateBookCommand command, AppDbContext db, IValidator<UpdateBookCommand> validator) =>
{
    if (id != command.Id) return Results.BadRequest("ID in URL and body must match");

    var validation = await validator.ValidateAsync(command);
    if (!validation.IsValid) return Results.BadRequest(validation.Errors);

    var handler = new UpdateBookHandler(db);
    var updated = await handler.HandleAsync(command);
    return updated is not null ? Results.Ok(updated) : Results.NotFound();
});

app.MapGet("/books/{id:int}", async (int id, AppDbContext db) =>
{
    var handler = new GetBookByIdHandler(db);
    var result = await handler.HandleAsync(new GetBookByIdQuery(id));
    return result is not null ? Results.Ok(result) : Results.NotFound();
});

app.MapGet("/books", async (int? page, int? pageSize, AppDbContext db) =>
{
    var handler = new GetAllBooksHandler(db);
    var result = await handler.HandleAsync(new GetAllBooksQuery(page ?? 1, pageSize ?? 10));
    return Results.Ok(result);
});

app.MapDelete("/books/{id:int}", async (int id, AppDbContext db) =>
{
    var handler = new DeleteBookHandler(db);
    var success = await handler.HandleAsync(new DeleteBookCommand(id));
    return success ? Results.NoContent() : Results.NotFound();
});

app.Run();
