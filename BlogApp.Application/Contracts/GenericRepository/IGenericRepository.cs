using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlogApp.Application.Helpers;
using Microsoft.EntityFrameworkCore.Query;

namespace BlogApp.Application.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByColumnAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task DeleteAsync(int id);
        void UpdateASync(T entity);
        Task<T> AddAsync(T entity);

        Task<PaginatedList<T>> GetPaginatedAsync(
        Expression<Func<T, bool>> predicate, 
        int pageNumber, 
        int pageSize, 
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        // Task<PaginatedList<T>> GetPaginated(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize);
    }
}