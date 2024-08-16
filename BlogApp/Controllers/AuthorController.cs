using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Application.Contracts.Services;
using BlogApp.Application.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BlogApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }


        [HttpPost]
        [Route("AddAuthor")]

        public async Task<IActionResult> AddAuthor(AuthorRequestDto requestDto)
        {
            var author = await _authorService.AddAuthor(requestDto);
            return StatusCode((int)author.statusCode, author.Success ? author.Data : author.Message);

        }

        [HttpDelete]
        [Route("DeleteAuthor")]

        public async Task<IActionResult> DeleteBlog(int AuthorId)
        {
            var result = await _authorService.DeleteAuthor(AuthorId);
            return StatusCode((int)result.statusCode, result.Success ? result.Message : result.Message);
        }
    }
}