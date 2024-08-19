using System.Threading.Tasks;
using BlogApp.Application.Command.Post;
using BlogApp.Application.Contracts.Services;
using BlogApp.Application.DTOs.Request;
using BlogApp.Application.Query.Post;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        
        private readonly IValidator<PostRequestDto> _validator;
        private readonly IMediator _mediator;

        public PostController( IValidator<PostRequestDto> validator, IMediator mediator)
        {
          
            _validator = validator;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost(PostRequestDto postRequest)
        {
            var validationResult = await _validator.ValidateAsync(postRequest);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);



            }

            // var result = await _postService.AddPost(postRequest);
             var result = await _mediator.Send(new CreatePost(postRequest));
            return StatusCode((int)result.statusCode, result.Success ? result.Data : result.Message);
        }

        [HttpDelete]
        [Route("DeletePost")]

        public async Task<IActionResult> DeletePost(int id)
        {
            var result = await _mediator.Send(new DeletePost(id));
            return StatusCode((int)result.statusCode, result.Success ? result.Message : result.Message);
        }

        [HttpPut]
        [Route("UpdatePost")]

        public async Task<IActionResult> UpdatePost(PostRequestDto requestDto)
        {
            var result = await _mediator.Send(new UpdatePost(requestDto));
            return StatusCode((int)result.statusCode, result.Success ? result.Message : result.Message);
        }

        [HttpGet]
        [Route("PostByBlogId")]

        public async Task<IActionResult> GetPostByBlogId(int blogId, int pageNumber, int pageSize)
        {
            var result = await _mediator.Send(new GetPostByBlogId(blogId, pageNumber, pageSize));
            return Ok(result);
        }
    }
}