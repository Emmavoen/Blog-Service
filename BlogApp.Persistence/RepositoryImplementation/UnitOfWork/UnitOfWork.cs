using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Application.Contracts.Repository;
using BlogApp.Application.Contracts.UnitOfWork;
using BlogApp.Persistence.DatabaseContext;

namespace BlogApp.Persistence.RepositoryImplementation.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IPostRepository PostRepository { get; }

        public IBlogRepository BlogRepository { get; }

        public IAuthorRepository AuthorRepository { get; }

        public UnitOfWork(AppDbContext context,IPostRepository postRepository, IBlogRepository blogRepository, IAuthorRepository authorRepository)
        {
            PostRepository = postRepository;
            BlogRepository = blogRepository;
            AuthorRepository = authorRepository;
            _context = context;

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}