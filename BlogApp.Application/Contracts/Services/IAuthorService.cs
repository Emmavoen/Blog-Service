using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApp.Application.DTOs.Request;
using BlogApp.Application.DTOs.Response;
using BlogApp.Application.Helpers;
using BlogApp.Domain.Entities;

namespace BlogApp.Application.Contracts.Services
{
    public interface IAuthorService
    {
        
        Task<Result<AuthorResponceDto>> AddAuthor(AuthorRequestDto requestDto);
        Task<Result<AuthorResponceDto>> DeleteAuthor(int id);

        Task<Result<AuthorResponceDto>> GetAuthorById(int id);
        Task<Result<AuthorResponceDto>> UpdateAuthor(AuthorRequestDto requestDto);
    }
}