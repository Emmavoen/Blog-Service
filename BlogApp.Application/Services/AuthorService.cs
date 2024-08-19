using System.Net;
using System.Threading.Tasks;
using BlogApp.Application.Contracts.Services;
using BlogApp.Application.Contracts.UnitOfWork;
using BlogApp.Application.DTOs.Request;
using BlogApp.Application.DTOs.Response;
using BlogApp.Application.Helpers;
using BlogApp.Domain.Entities;

namespace BlogApp.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<AuthorResponceDto>> AddAuthor(AuthorRequestDto requestDto)
        {
            var AuthorExist = await _unitOfWork.AuthorRepository.GetByColumnAsync(x => x.Name == requestDto.Name && x.Email == requestDto.Email);
            if (AuthorExist != null)
            {
                return Result<AuthorResponceDto>.ErrorResult("Author Already Exist", HttpStatusCode.BadRequest);
                
            }

            var newAuthor =  new Author()
            {
                Name = requestDto.Name,
                Email = requestDto.Email,
                
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

        public async Task<Result<AuthorResponceDto>> DeleteAuthor(int id)
        {
            var exist =  await _unitOfWork.AuthorRepository.GetByColumnAsync(x => x.Id == id);
            if (exist == null)  
            {
                return Result<AuthorResponceDto>.ErrorResult("Bad Request", HttpStatusCode.BadRequest);
            }

             await _unitOfWork.AuthorRepository.DeleteAsync(id);
             await _unitOfWork.Save();
           
            return Result<AuthorResponceDto>.SuccessResult("Success", HttpStatusCode.OK);
        }

        public async Task<Result<AuthorResponceDto>> GetAuthorById(int id)
        {
            var exist =  await _unitOfWork.AuthorRepository.GetByColumnAsync(x => x.Id == id);
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
            //throw new System.NotImplementedException();
        }

        public async Task<Result<AuthorResponceDto>> UpdateAuthor(AuthorRequestDto requestDto)
        {
            var exist = await _unitOfWork.AuthorRepository.GetByColumnAsync(x => x.Id == requestDto.Id);

            if(exist == null)
            {
                return Result<AuthorResponceDto>.ErrorResult("Invalid Credentials", HttpStatusCode.BadRequest);
            };

            exist.Name = requestDto.Name;
            exist.Email = requestDto.Email;

             _unitOfWork.AuthorRepository.UpdateASync(exist);
             await _unitOfWork.Save();
            return Result<AuthorResponceDto>.SuccessResult("Success", HttpStatusCode.OK);
        }
    }
}