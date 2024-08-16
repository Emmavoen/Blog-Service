using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Application.Contracts.Repository;
using BlogApp.Domain.Entities;
using BlogApp.Persistence.DatabaseContext;
using BlogApp.Persistence.RepositoryImplementation.GenericRepository;

namespace BlogApp.Persistence.RepositoryImplementation.Repository
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(AppDbContext _context) : base(_context)
        {
            
        }
    }
}