using System.Threading.Tasks;
using BlogApp.Application.Command.Author;
using BlogApp.Application.Contracts.Services;
using BlogApp.Application.DTOs.Request;
using BlogApp.Application.Query.Author;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BlogApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IValidator<AuthorRequestDto> _validator;
        private readonly IMediator _mediator;

        public AuthorController(IAuthorService authorService, IValidator<AuthorRequestDto> validator, IMediator mediator)
        {
            _authorService = authorService;
            _validator = validator;
            _mediator = mediator;
        }


        [HttpPost]
        [Route("AddAuthor")]

        public async Task<IActionResult> AddAuthor(AuthorRequestDto requestDto)
        {
            var validationResult = await _validator.ValidateAsync(requestDto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);



            }

            var author = await _mediator.Send(new CreateAuthor(requestDto));
            return StatusCode((int)author.statusCode, author.Success ? author.Data : author.Message);

        }

        [HttpDelete]
        [Route("DeleteAuthor")]

        public async Task<IActionResult> DeleteAuthor(int AuthorId)
        {
            var result = await _mediator.Send(new DeleteAuthor(AuthorId));
            return StatusCode((int)result.statusCode, result.Success ? result.Message : result.Message);
        }

        [HttpGet]
        [Route("GetAuthorById")]

        public async Task<IActionResult> GetByAuthorId( int AuthorId)
        {
            var result = await _mediator.Send(new GetAuthorById(AuthorId));
            return StatusCode((int)result.statusCode, result.Success ? result.Data : result.Message); 
        }

        [HttpPut]
        [Route("UpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor(AuthorRequestDto requestDto)
        {
            var result = await _mediator.Send(new UpdateAuthor(requestDto));
            return StatusCode((int)result.statusCode, result.Success ? result.Message : result.Message);
        }
    }
}