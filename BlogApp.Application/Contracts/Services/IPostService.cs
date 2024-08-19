using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Application.DTOs.Request;
using BlogApp.Application.DTOs.Response;
using BlogApp.Application.Helpers;

namespace BlogApp.Application.Contracts.Services
{
    public interface IPostService
    {
        Task<PaginatedList<PostResponseDto>> GetPostByBlogId(int BlogId, int pageNumber, int pageSize);
        Task<Result<PostResponseDto>> AddPost(PostRequestDto requestDto);
        Task<Result<PostResponseDto>> DeletePost(int id);

        Task<Result<PostResponseDto>> GetPostById(int id);
        Task<Result<PostResponseDto>> UpdatePost(PostRequestDto requestDto);
    }
}