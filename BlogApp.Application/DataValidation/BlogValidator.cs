using System;
using BlogApp.Application.DTOs.Request;
using FluentValidation;

namespace BlogApp.Application.DataValidation
{


    public class BlogValidator : AbstractValidator<BlogRequestDto>
{
    public BlogValidator()
    {
        // Validate Name
        RuleFor(blog => blog.Name)
            .NotEmpty().WithMessage("Blog name is required.")
            .Length(2, 100).WithMessage("Blog name must be between 2 and 100 characters.");

        // Validate URL
        RuleFor(blog => blog.Url)
            .NotEmpty().WithMessage("URL is required.")
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("A valid URL is required.");

        // Validate AuthorId
        RuleFor(blog => blog.AuthorId)
            .GreaterThan(0).WithMessage("A valid AuthorId is required.");
    }
}

}