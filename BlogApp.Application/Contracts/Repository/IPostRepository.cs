using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Domain.Entities;

namespace BlogApp.Application.Contracts.Repository
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        
    }
}