using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BlogApp.Application.Contracts.Services;
using BlogApp.Application.Contracts.UnitOfWork;
using BlogApp.Application.DTOs.Request;
using BlogApp.Application.DTOs.Response;
using BlogApp.Application.Helpers;
using BlogApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<PostResponseDto>> AddPost(PostRequestDto requestDto)
        {
             var postExist = await _unitOfWork.PostRepository.GetByColumnAsync(x => x.Title == requestDto.Title && x.DatePublished == requestDto.DatePublished);
            if (postExist != null)
            {
                return Result<PostResponseDto>.ErrorResult("Post Already Exist", HttpStatusCode.BadRequest);
                
            }

            var newPost =  new Post()
            {
                Title = requestDto.Title,
                Content = requestDto.Content,
                BlogId = requestDto.BlogId,
                DatePublished = requestDto.DatePublished,

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

        public async Task<Result<PostResponseDto>> DeletePost(int id)
        {
             var exist =  await _unitOfWork.PostRepository.GetByColumnAsync(x => x.Id == id);
            if (exist == null)  
            {
                return Result<PostResponseDto>.ErrorResult("Bad Request", HttpStatusCode.BadRequest);
            }

             await _unitOfWork.AuthorRepository.DeleteAsync(id);
             await _unitOfWork.Save();
           
            return Result<PostResponseDto>.SuccessResult("Success", HttpStatusCode.OK);
        }

        public Task<Result<PostResponseDto>> GetPostById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<PostResponseDto>> UpdatePost(PostRequestDto requestDto)
        {
            var exist = await _unitOfWork.PostRepository.GetByColumnAsync(x => x.Id == requestDto.Id);

            if(exist == null)
            {
                return Result<PostResponseDto>.ErrorResult("Invalid Credentials", HttpStatusCode.BadRequest);
            };

            exist.Content = requestDto.Content;
            exist.Title = requestDto.Title;
            exist.BlogId = requestDto.BlogId;
            exist.DatePublished = requestDto.DatePublished;

             _unitOfWork.PostRepository.UpdateASync(exist);
             await _unitOfWork.Save();
            return Result<PostResponseDto>.SuccessResult("Success", HttpStatusCode.OK);
        }

        public async Task<PaginatedList<PostResponseDto>> GetPostByBlogId(int BlogId, int pageNumber, int pageSize)
        {


            // Call the repository method to get the paginated list
            var listPost = await _unitOfWork.PostRepository.GetPaginatedAsync(x => x.BlogId == BlogId, pageNumber, pageSize, include: query => query.Include(b => b.Blog));

            var dtoItems = listPost.Items.Select(post => new PostResponseDto
            {
                Content = post.Content,
                Title = post.Title,
                DatePublished = post.DatePublished,
                BlogName = post.Blog.Name,



            }).ToList();

            return new PaginatedList<PostResponseDto>(dtoItems, listPost.TotalCount, pageNumber, pageSize);
        }

    }
}