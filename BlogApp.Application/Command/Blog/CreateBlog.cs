using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Application.Contracts.UnitOfWork;
using BlogApp.Application.DTOs.Request;
using BlogApp.Application.DTOs.Response;
using BlogApp.Application.Helpers;
using MediatR;

namespace BlogApp.Application.Command.Blog
{
    public class CreateBlog : IRequest<Result<BlogResponceDto>>
    {
        internal readonly BlogRequestDto requestDto;

        public CreateBlog(BlogRequestDto requestDto)
        {
            this.requestDto = requestDto;
        }
    }

    public class CreateBlogHandler : IRequestHandler<CreateBlog, Result<BlogResponceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBlogHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<BlogResponceDto>> Handle(CreateBlog request, CancellationToken cancellationToken)
        {
             var blogExist = await _unitOfWork.BlogRepository.GetByColumnAsync(x => x.Name == request.requestDto.Name && x.Url == request.requestDto.Url);
            if (blogExist != null)
            {
                return Result<BlogResponceDto>.ErrorResult("Blog Already Exist", HttpStatusCode.BadRequest);

            }

            var newBlog = new Domain.Entities.Blog()
            {
                Name = request.requestDto.Name,
                Url = request.requestDto.Url,
                AuthorId = request.requestDto.AuthorId
            };
            await _unitOfWork.BlogRepository.AddAsync(newBlog);
            var save = await _unitOfWork.Save();
            if (save < 1)
            {
                return Result<BlogResponceDto>.ErrorResult("Something went wrong", HttpStatusCode.InternalServerError);
            }

            var blogResponse = new BlogResponceDto
            {
                Name = newBlog.Name,
                Id = newBlog.Id,
                Url = newBlog.Url

            };

            return Result<BlogResponceDto>.SuccessResult(blogResponse, HttpStatusCode.OK);
        }
    }
}