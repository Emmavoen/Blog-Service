using BlogApp.Application.Contracts.Services;
using BlogApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;
namespace BlogApp.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection RegisterApplicationService(this IServiceCollection services)
        {

            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IAuthorService, AuthorService>();

            return services;
     
            ;
        }
    }
}
