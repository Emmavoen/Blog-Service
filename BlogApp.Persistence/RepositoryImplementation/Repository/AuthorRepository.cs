using BlogApp.Application.Contracts.Repository;
using BlogApp.Domain.Entities;
using BlogApp.Persistence.DatabaseContext;
using BlogApp.Persistence.RepositoryImplementation.GenericRepository;

namespace BlogApp.Persistence.RepositoryImplementation.Repository
{
    public class AuthorRepository : GenericRepository<Author> , IAuthorRepository
    {
        public AuthorRepository(AppDbContext _context) : base(_context)
        {
            
        }
    }
}