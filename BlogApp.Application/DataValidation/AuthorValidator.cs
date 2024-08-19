using BlogApp.Application.DTOs.Request;
using FluentValidation;

namespace BlogApp.Application.DataValidation
{


    public class AuthorValidator : AbstractValidator<AuthorRequestDto>
{
    public AuthorValidator()
    {
        // Validate Name
        RuleFor(author => author.Name)
            .NotEmpty().WithMessage("Author name is required.")
            .Length(2, 100).WithMessage("Author name must be between 2 and 100 characters.");

        // Validate Email
        RuleFor(author => author.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");
    }
}

}