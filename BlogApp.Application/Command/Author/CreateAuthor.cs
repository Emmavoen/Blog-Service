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
    public class CreateAuthor : IRequest<Result<AuthorResponceDto>>
    {
        public  AuthorRequestDto RequestDto {get;}

    //parameter
        public CreateAuthor(AuthorRequestDto _requestDto)
        {
            RequestDto = _requestDto;
        }
    
    }

    public class CreateAuthorHandler : IRequestHandler<CreateAuthor, Result<AuthorResponceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAuthorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async  Task<Result<AuthorResponceDto>> Handle(CreateAuthor request, CancellationToken cancellationToken)
        {
            

             var AuthorExist = await _unitOfWork.AuthorRepository.GetByColumnAsync(x => x.Name == request.RequestDto.Name && x.Email == request.RequestDto.Email);
            if (AuthorExist != null)
            {
                return Result<AuthorResponceDto>.ErrorResult("Author Already Exist", HttpStatusCode.BadRequest);
                
            }

            var newAuthor =  new Domain.Entities.Author()
            {
                Name = request.RequestDto.Name,
                Email = request.RequestDto.Email,
                
            };
            await _unitOfWork.AuthorRepository.AddAsync(newAuthor);
            var save =  await _unitOfWork.Save();
            if(save  < 1)
            {
                return Result<AuthorResponceDto>.ErrorResult("Something went wrong",HttpStatusCode.InternalServerError);
            }

            var AuthorResponse = new AuthorResponceDto
            {
                Name = newAuthor.Name,
                Id = newAuthor.Id,
                Email = newAuthor.Email

            };

            return Result<AuthorResponceDto>.SuccessResult(AuthorResponse , HttpStatusCode.OK);




        }
    }

}