using System;
using BlogApp.Application.DTOs.Request;
using FluentValidation;
namespace BlogApp.Application.DataValidation
{


    public class PostRequestDtoValidator : AbstractValidator<PostRequestDto>
    {
        public PostRequestDtoValidator()
        {
            // Validate Title
            RuleFor(post => post.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(2, 255).WithMessage("Title must be between 2 and 255 characters.");

            // Validate Content
            RuleFor(post => post.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MinimumLength(10).WithMessage("Content must be at least 10 characters long.");

        // Validate BlogId
        RuleFor(post => post.BlogId)
            .GreaterThan(0).WithMessage("A valid BlogId is required.");

        // Validate DatePublished
        RuleFor(post => post.DatePublished)
            .NotEmpty().WithMessage("DatePublished is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("DatePublished cannot be in the future.");


        }

    }

}