using System.Threading.Tasks;
using BlogApp.Application.Command.Blog;
using BlogApp.Application.Contracts.Services;
using BlogApp.Application.DTOs.Request;
using BlogApp.Application.Query.Blog;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IValidator<BlogRequestDto> _validator;
        private readonly IMediator _mediator;

        public BlogController(IBlogService blogService, IValidator<BlogRequestDto> validator, IMediator mediator)
        {
            _blogService = blogService;
            _validator = validator;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CreateBlog")]

        public async Task<IActionResult> CreateBlog(BlogRequestDto requestDto)
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

            var result = await _mediator.Send(new CreateBlog (requestDto));

            return StatusCode((int)result.statusCode, result.Success ? result.Data : result.Message);

        }

        [HttpDelete]
        [Route("DeleteBlog")]

        public async Task<IActionResult> DeleteBlog(int blogId)
        {
            var result = await _mediator.Send(new DeleteBlog(blogId));
            return StatusCode((int)result.statusCode, result.Success ? result.Message : result.Message);
        }

        [HttpGet]
        [Route("GetByAuthorId")]

        public async Task<IActionResult> GetByAuthorId(int authorId, int pageNumber, int pageSize)
        {
            var result = await _mediator.Send(new GetBlogByAuthorId(authorId,pageNumber,pageSize));
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateBlog")]

        public async Task<IActionResult> UpdateBlog(int id, string name, string url)
        {
            var result = await _mediator.Send(new UpdateBlog(id, name, url));
            return StatusCode((int)result.statusCode, result.Success ? result.Message : result.Message);
        }
    }
}