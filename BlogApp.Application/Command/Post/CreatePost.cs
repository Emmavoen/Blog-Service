using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CreatePost : IRequest<Result<PostResponseDto>>
    {
        internal readonly PostRequestDto requestDto;

        public CreatePost(PostRequestDto requestDto)
        {
            this.requestDto = requestDto;
        }
    }

    public class CreatePostHandler : IRequestHandler<CreatePost, Result<PostResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePostHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<PostResponseDto>> Handle(CreatePost request, CancellationToken cancellationToken)
        {
            var postExist = await _unitOfWork.PostRepository.GetByColumnAsync(x => x.Title == request.requestDto.Title && x.DatePublished == request.requestDto.DatePublished);
            if (postExist != null)
            {
                return Result<PostResponseDto>.ErrorResult("Post Already Exist", HttpStatusCode.BadRequest);
                
            }

            var newPost =  new Domain.Entities.Post()
            {
                Title = request.requestDto.Title,
                Content = request.requestDto.Content,
                BlogId = request.requestDto.BlogId,
                DatePublished = request.requestDto.DatePublished,

            };
            await _unitOfWork.PostRepository.AddAsync(newPost);
            var save =  await _unitOfWork.Save();
            if(save  < 1)
            {
                return Result<PostResponseDto>.ErrorResult("Something went wrong",HttpStatusCode.InternalServerError);
            }

            var postResponse = new PostResponseDto
            {
                Title = newPost.Title,
                Content = newPost.Content,
                DatePublished = newPost.DatePublished,
            };

            return Result<PostResponseDto>.SuccessResult(postResponse , HttpStatusCode.OK);
        }
    }
}