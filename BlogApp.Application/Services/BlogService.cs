using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using BlogApp.Application.Contracts.Services;
using BlogApp.Application.Contracts.UnitOfWork;
using BlogApp.Application.DTOs.Request;
using BlogApp.Application.DTOs.Response;
using BlogApp.Application.Helpers;
using BlogApp.Domain.Entities;

namespace BlogApp.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<BlogResponceDto>> AddBlog(BlogRequestDto requestDto)
        {
            var blogExist = await _unitOfWork.BlogRepository.GetByColumnAsync(x => x.Name == requestDto.Name && x.Url == requestDto.Url);
            if (blogExist != null)
            {
                return Result<BlogResponceDto>.ErrorResult("Blog Already Exist", HttpStatusCode.BadRequest);
                
            }

            var newBlog =  new Blog()
            {
                Name = requestDto.Name,
                Url = requestDto.Url,
                AuthorId = requestDto.AuthorId
            };
            await _unitOfWork.BlogRepository.AddAsync(newBlog);
            var save =  await _unitOfWork.Save();
            if(save  < 1)
            {
                return Result<BlogResponceDto>.ErrorResult("Something went wrong",HttpStatusCode.InternalServerError);
            }

            var blogResponse = new BlogResponceDto
            {
                Name = newBlog.Name,
                Id = newBlog.Id,
                Url = newBlog.Url

            };

            return Result<BlogResponceDto>.SuccessResult(blogResponse , HttpStatusCode.OK);
        }

        public async Task<Result<BlogResponceDto>> DeleteBlog(int id)
        {
            var exist =  await _unitOfWork.BlogRepository.GetByColumnAsync(x => x.Id == id);
            if (exist == null)  
            {
                return Result<BlogResponceDto>.ErrorResult("Bad Request", HttpStatusCode.BadRequest);
            }

             await _unitOfWork.BlogRepository.DeleteAsync(id);
             await _unitOfWork.Save();
           
            return Result<BlogResponceDto>.SuccessResult("Success", HttpStatusCode.OK);

             
        }

        // public async Task<Result<IEnumerable<BlogResponceDto>> GetAllBlogByAuthorId(int id)
        // {
        //     var blog = await _unitOfWork.BlogRepository.GetAllAsync(x=> x.AuthorId == id);
        //     if(blog == null)
        //     {
        //         return Result<IEnumerable<BlogResponceDto>>.ErrorResult("Bad Request", HttpStatusCode.BadRequest);
        //     };

        //     var blogResponce = new BlogResponceDto
        //     {
        //         Id = blog.Id,
        //     } ;
        //     return Result<IEnumerable<BlogResponceDto>>.SuccessResult(blog, HttpStatusCode.OK);
        // }   

        public async Task<PaginatedList<BlogResponceDto>> GetPaginatedPaymentsBySenderAccountAsync(int authorId, int pageNumber, int pageSize)
    {
         var query = _dbContext.Blogs
        .Include(b => b.Author)
        .Where(b => b.AuthorId == authorId);

        // Call the repository method to get the paginated list
        var listBlog = await _unitOfWork.BlogRepository.GetPaginated(x => x.AuthorId == authorId, pageNumber, pageSize);
    
        var dtoItems = listBlog.Items.Select(blog => new BlogResponceDto
                                   {
                                       Name = blog.Name,
                                       Url = blog.Url,
                                       AuthorName = blog.Author.Name,

                                       
                                       
                                    }).ToList();

        return new PaginatedList<BlogResponceDto>(dtoItems,listBlog.TotalCount, pageNumber,pageSize);
    }

        public async Task<Result<BlogResponceDto>> UpdateBlog(int id,string name, string url)
        {
            var exist = await _unitOfWork.BlogRepository.GetByColumnAsync(x => x.Id == id);

            if(exist == null)
            {
                return Result<BlogResponceDto>.ErrorResult("Invalid Credentials", HttpStatusCode.BadRequest);
            };

            exist.Name = name;
            exist.Url = url;

             _unitOfWork.BlogRepository.UpdateASync(exist);
             await _unitOfWork.Save();
            return Result<BlogResponceDto>.SuccessResult("Success", HttpStatusCode.OK);
        }
    }
}