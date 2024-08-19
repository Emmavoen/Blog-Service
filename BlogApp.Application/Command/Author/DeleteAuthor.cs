using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Application.Contracts.UnitOfWork;
using BlogApp.Application.DTOs.Response;
using BlogApp.Application.Helpers;
using MediatR;

namespace BlogApp.Application.Command.Author
{
    public class DeleteAuthor: IRequest<Result<AuthorResponceDto>>
    {
        public readonly int id;

        public DeleteAuthor(int id)
        {
            this.id = id;
        }
    }

    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthor, Result<AuthorResponceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAuthorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<AuthorResponceDto>> Handle(DeleteAuthor request, CancellationToken cancellationToken)
        {
            var exist =  await _unitOfWork.AuthorRepository.GetByColumnAsync(x => x.Id == request.id);
            if (exist == null)  
            {
                return Result<AuthorResponceDto>.ErrorResult("Bad Request", HttpStatusCode.BadRequest);
            }

             await _unitOfWork.AuthorRepository.DeleteAsync(request.id);
             await _unitOfWork.Save();
           
            return Result<AuthorResponceDto>.SuccessResult("Success", HttpStatusCode.OK);
        }
    }
}