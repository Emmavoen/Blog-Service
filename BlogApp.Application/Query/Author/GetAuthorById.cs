using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Application.Contracts.UnitOfWork;
using BlogApp.Application.DTOs.Response;
using BlogApp.Application.Helpers;
using MediatR;

namespace BlogApp.Application.Query.Author
{
    public class GetAuthorById : IRequest<Result<AuthorResponceDto>>
    {
        internal readonly int id;

        public GetAuthorById(int id)
        {
            this.id = id;
        }
    }

    public class GetAuthorByIdHandler : IRequestHandler<GetAuthorById, Result<AuthorResponceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAuthorByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<AuthorResponceDto>> Handle(GetAuthorById request, CancellationToken cancellationToken)
        {
            var exist =  await _unitOfWork.AuthorRepository.GetByColumnAsync(x => x.Id == request.id);
            if (exist == null)  
            {
                return Result<AuthorResponceDto>.ErrorResult("Bad Request", HttpStatusCode.BadRequest);
            }

            var author  = new AuthorResponceDto()
            {
                Id = exist.Id,
                Email = exist.Email,
                Name = exist.Name,

            };
            return Result<AuthorResponceDto>.SuccessResult(author, HttpStatusCode.OK);
        }
    }
}