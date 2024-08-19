using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Application.Contracts.UnitOfWork;
using BlogApp.Application.DTOs.Response;
using BlogApp.Application.Helpers;
using MediatR;

namespace BlogApp.Application.Command.Post
{
    public class DeletePost : IRequest<Result<PostResponseDto>>
    {
        
        
        public int id;
        public DeletePost(int id)
        {
            this.id = id;
            
        }
    }

    public  class DeletePostHandler : IRequestHandler<DeletePost, Result<PostResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePostHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<PostResponseDto>> Handle(DeletePost request, CancellationToken cancellationToken)
        {
            var exist =  await _unitOfWork.PostRepository.GetByColumnAsync(x => x.Id == request.id);
            if (exist == null)  
            {
                return Result<PostResponseDto>.ErrorResult("Bad Request", HttpStatusCode.BadRequest);
            }

             await _unitOfWork.AuthorRepository.DeleteAsync(request.id);
             await _unitOfWork.Save();
           
            return Result<PostResponseDto>.SuccessResult("Success", HttpStatusCode.OK);
        }
    }
}

