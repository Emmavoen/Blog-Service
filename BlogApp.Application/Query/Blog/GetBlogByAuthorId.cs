using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Application.Contracts.UnitOfWork;
using BlogApp.Application.DTOs.Response;
using BlogApp.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Query.Blog
{
    public class GetBlogByAuthorId : IRequest<PaginatedList<BlogResponceDto>>
    {
        internal readonly int authorId;
        internal readonly int pageNumber;
        internal readonly int pageSize;

        public GetBlogByAuthorId(int authorId, int pageNumber, int pageSize)
        {
            this.authorId = authorId;
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }
    }

    public class GetBlogByAuthorIdHandler : IRequestHandler<GetBlogByAuthorId, PaginatedList<BlogResponceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBlogByAuthorIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async  Task<PaginatedList<BlogResponceDto>> Handle(GetBlogByAuthorId request, CancellationToken cancellationToken)
        {
            var listBlog = await _unitOfWork.BlogRepository.GetPaginatedAsync(x => x.AuthorId == request.authorId, request.pageNumber, request.pageSize, include: query => query.Include(b => b.Author));

            var dtoItems = listBlog.Items.Select(blog => new BlogResponceDto
            {
                Name = blog.Name,
                Url = blog.Url,
                AuthorName = blog.Author.Name,



            }).ToList();

            return new PaginatedList<BlogResponceDto>(dtoItems, listBlog.TotalCount, request.pageNumber, request.pageSize);
        }
    }
}