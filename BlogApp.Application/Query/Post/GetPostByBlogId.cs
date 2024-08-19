using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Application.Contracts.UnitOfWork;
using BlogApp.Application.DTOs.Response;
using BlogApp.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Query.Post
{
    public class GetPostByBlogId : IRequest<PaginatedList<PostResponseDto>>
    {
        internal readonly int blogId;
        internal readonly int pageNumber;
        internal readonly int pageSize;

        public GetPostByBlogId(int BlogId, int pageNumber, int pageSize)
        {
            blogId = BlogId;
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }
    }

    public class GetPostByBlogIdHandler : IRequestHandler<GetPostByBlogId, PaginatedList<PostResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPostByBlogIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async  Task<PaginatedList<PostResponseDto>> Handle(GetPostByBlogId request, CancellationToken cancellationToken)
        {
            var listPost = await _unitOfWork.PostRepository.GetPaginatedAsync(x => x.BlogId == request.blogId, request.pageNumber, request.pageSize, include: query => query.Include(b => b.Blog));

            var dtoItems = listPost.Items.Select(post => new PostResponseDto
            {
                Content = post.Content,
                Title = post.Title,
                DatePublished = post.DatePublished,
                BlogName = post.Blog.Name,



            }).ToList();

            return new PaginatedList<PostResponseDto>(dtoItems, listPost.TotalCount, request.pageNumber, request.pageSize);
        }
    }
}