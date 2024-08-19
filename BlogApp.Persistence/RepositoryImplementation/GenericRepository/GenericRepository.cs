using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlogApp.Application.Contracts;
using BlogApp.Application.Helpers;
using BlogApp.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace BlogApp.Persistence.RepositoryImplementation.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected DbSet<T> _dbSet;

        public GenericRepository(AppDbContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            var result = await _dbSet.AddAsync(entity);
            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _dbSet.FindAsync(id);
            _dbSet.Remove(existing);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> GetByColumnAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public void UpdateASync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

        }

        // public async Task<PaginatedList<T>> GetPaginated(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        // {
        //     IQueryable<T> query = _context.Set<T>();

        //     if (include != null)
        //     {
        //         query = include(query);
        //     }

        //     var query = query.Where(predicate).AsQueryable();
        //     var count = await query.CountAsync();

        //     var items = await query.Skip((pageNumber - 1) * pageSize)
        //                            .Take(pageSize)
        //                            .ToListAsync();

        //     return new PaginatedList<T>(items, count, pageNumber, pageSize);
        // }


         public async Task<PaginatedList<T>> GetPaginatedAsync(
        Expression<Func<T, bool>> predicate, 
        int pageNumber, 
        int pageSize, 
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (include != null)
        {
            query = include(query);
        }

        query = query.Where(predicate);

        var count = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }

    }
}