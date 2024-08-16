using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Application.Contracts.Repository;

namespace BlogApp.Application.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository{ get; }
        IBlogRepository BlogRepository{ get; }
        IAuthorRepository AuthorRepository{ get; }

        Task<int> Save();
    }
}