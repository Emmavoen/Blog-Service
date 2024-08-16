using BlogApp.Application.Contracts.Repository;
using BlogApp.Application.Contracts.UnitOfWork;
using BlogApp.Persistence.DatabaseContext;
using BlogApp.Persistence.RepositoryImplementation.Repository;
using BlogApp.Persistence.RepositoryImplementation.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection RegisterPersistenceService(this IServiceCollection services, IConfiguration conn)
        {

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                    conn.GetConnectionString("Conn")));
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            return services;
            
            ;
        }
    }
}
