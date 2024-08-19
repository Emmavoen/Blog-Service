using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Application.Contracts.UnitOfWork;
using BlogApp.Application.DTOs.Request;
using BlogApp.Application.DTOs.Response;
using BlogApp.Application.Helpers;
using MediatR;

namespace BlogApp.Application.Command.Post
{
    public class UpdatePost : IRequest<Result<PostResponseDto>>
    {
        internal readonly PostRequestDto requestDto;

        public UpdatePost(PostRequestDto requestDto)
        {
            this.requestDto = requestDto;
        }
    }

    public class UpdatePostHandler : IRequestHandler<UpdatePost, Result<PostResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePostHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<PostResponseDto>> Handle(UpdatePost request, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.PostRepository.GetByColumnAsync(x => x.Id == request.requestDto.Id);

            if(exist == null)
            {
                return Result<PostResponseDto>.ErrorResult("Invalid Credentials", HttpStatusCode.BadRequest);
            };

            exist.Content = request.requestDto.Content;
            exist.Title = request.requestDto.Title;
            exist.BlogId = request.requestDto.BlogId;
            exist.DatePublished = request.requestDto.DatePublished;

             _unitOfWork.PostRepository.UpdateASync(exist);
             await _unitOfWork.Save();
            return Result<PostResponseDto>.SuccessResult("Success", HttpStatusCode.OK);
        }
    }
}