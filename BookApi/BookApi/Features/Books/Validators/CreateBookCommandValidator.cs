using BookApi.Features.Books.Commands;
using FluentValidation;

namespace BookApi.Features.Books.Validators;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Author).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Year).LessThanOrEqualTo(2004);
    }
}