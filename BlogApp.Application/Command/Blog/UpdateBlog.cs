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
    public class UpdateBlog : IRequest<Result<BlogResponceDto>>
    {
        internal readonly int id;
        internal readonly string name;
        internal readonly string url;

        public UpdateBlog(int id,string name, string url)
        {
            this.id = id;
            this.name = name;
            this.url = url;
        }
    }
    public class UpdateBlogHandler : IRequestHandler<UpdateBlog, Result<BlogResponceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBlogHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<BlogResponceDto>> Handle(UpdateBlog request, CancellationToken cancellationToken)
        {
             var exist = await _unitOfWork.BlogRepository.GetByColumnAsync(x => x.Id == request.id);

            if (exist == null)
            {
                return Result<BlogResponceDto>.ErrorResult("Invalid Credentials", HttpStatusCode.BadRequest);
            };

            exist.Name = request.name;
            exist.Url = request.url;

            _unitOfWork.BlogRepository.UpdateASync(exist);
            await _unitOfWork.Save();
            return Result<BlogResponceDto>.SuccessResult("Success", HttpStatusCode.OK);
        }
    }
}