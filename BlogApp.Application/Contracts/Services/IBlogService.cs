using System.Threading.Tasks;
using BlogApp.Application.DTOs.Request;
using BlogApp.Application.DTOs.Response;
using BlogApp.Application.Helpers;

namespace BlogApp.Application.Contracts.Services
{
    public interface IBlogService
    {
        Task<Result<BlogResponceDto>> AddBlog(BlogRequestDto requestDto);
        Task<Result<BlogResponceDto>> DeleteBlog(int id);

        Task<PaginatedList<BlogResponceDto>> GetBlogByAuthorId(int authorId, int pageNumber, int pageSize);
        Task<Result<BlogResponceDto>> UpdateBlog(int id,string name, string url);


    }
}