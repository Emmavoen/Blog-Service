using System.Threading.Tasks;
using BlogApp.Application.Contracts.Services;
using BlogApp.Application.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
            
        }

        [HttpPost]
        [Route("CreateBlog")]

        public async Task<IActionResult> CreateBlog(BlogRequestDto requestDto)
        {
            var result = await _blogService.AddBlog(requestDto);
            return StatusCode((int)result.statusCode, result.Success ? result.Data : result.Message);

        }

        [HttpDelete]
        [Route("DeleteBlog")]

        public async Task<IActionResult> DeleteBlog(int blogId)
        {
            var result = await _blogService.DeleteBlog(blogId);
            return StatusCode((int)result.statusCode, result.Success ? result.Message : result.Message);
        }

        [HttpGet]
        [Route("GetByAuthorId")]

        public async Task<IActionResult> GetByAuthorId(int authorId, int pageNumber, int pageSize)
        {
            var result = await _blogService.GetPaginatedPaymentsBySenderAccountAsync(authorId,pageNumber,pageSize);
            return Ok(result);
        }
    }
}