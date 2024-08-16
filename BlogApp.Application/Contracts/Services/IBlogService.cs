using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApp.Application.DTOs.Request;
using BlogApp.Application.DTOs.Response;
using BlogApp.Application.Helpers;
using BlogApp.Domain.Entities;

namespace BlogApp.Application.Contracts.Services
{
    public interface IBlogService
    {
        Task<Result<BlogResponceDto>> AddBlog(BlogRequestDto requestDto);
        Task<Result<BlogResponceDto>> DeleteBlog(int id);

        Task<PaginatedList<BlogResponceDto>> GetPaginatedPaymentsBySenderAccountAsync(int authorId, int pageNumber, int pageSize);
        Task<Result<BlogResponceDto>> UpdateBlog(int id,string name, string url);


    }
}