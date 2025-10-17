using BookApi.Features.Books.Commands;
using FluentValidation;

namespace BookApi.Features.Books.Validators;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Author).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Year).InclusiveBetween(1000, DateTime.Today.Year);
    }
}