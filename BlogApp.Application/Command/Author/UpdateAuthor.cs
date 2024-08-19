using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Application.Contracts.UnitOfWork;
using BlogApp.Application.DTOs.Request;
using BlogApp.Application.DTOs.Response;
using BlogApp.Application.Helpers;
using MediatR;

namespace BlogApp.Application.Command.Author
{
    public class UpdateAuthor : IRequest<Result<AuthorResponceDto>>
    {
        internal readonly AuthorRequestDto requestDto;

        public UpdateAuthor(AuthorRequestDto requestDto)
        {
            this.requestDto = requestDto;
        }
    }

    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthor, Result<AuthorResponceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAuthorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async  Task<Result<AuthorResponceDto>> Handle(UpdateAuthor request, CancellationToken cancellationToken)
        {
            
             var exist = await _unitOfWork.AuthorRepository.GetByColumnAsync(x => x.Id == request.requestDto.Id);

            if(exist == null)
            {
                return Result<AuthorResponceDto>.ErrorResult("Invalid Credentials", HttpStatusCode.BadRequest);
            };

            exist.Name = request.requestDto.Name;
            exist.Email = request.requestDto.Email;

             _unitOfWork.AuthorRepository.UpdateASync(exist);
             await _unitOfWork.Save();
            return Result<AuthorResponceDto>.SuccessResult("Success", HttpStatusCode.OK);
        }
    }
}