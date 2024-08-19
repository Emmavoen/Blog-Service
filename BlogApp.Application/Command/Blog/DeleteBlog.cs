using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Application.Contracts.UnitOfWork;
using BlogApp.Application.DTOs.Response;
using BlogApp.Application.Helpers;
using MediatR;

namespace BlogApp.Application.Command.Blog
{
    public class DeleteBlog : IRequest<Result<BlogResponceDto>>
    {
        internal readonly int id;

        public DeleteBlog(int id)
        {
            this.id = id;
        }
    }

    public class DeleteBlogHandler : IRequestHandler<DeleteBlog, Result<BlogResponceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBlogHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async  Task<Result<BlogResponceDto>> Handle(DeleteBlog request, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.BlogRepository.GetByColumnAsync(x => x.Id == request.id);
            if (exist == null)
            {
                return Result<BlogResponceDto>.ErrorResult("Bad Request", HttpStatusCode.BadRequest);
            }

            await _unitOfWork.BlogRepository.DeleteAsync(request.id);
            await _unitOfWork.Save();

            return Result<BlogResponceDto>.SuccessResult("Success", HttpStatusCode.OK);
        }
    }
}