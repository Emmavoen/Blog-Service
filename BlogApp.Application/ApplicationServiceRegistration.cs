using System.Reflection;
using BlogApp.Application.Contracts.Services;
using BlogApp.Application.DataValidation;
using BlogApp.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
namespace BlogApp.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection RegisterApplicationService(this IServiceCollection services)
        {

            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPostService, PostService>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssemblyContaining<PostRequestDtoValidator>();

            return services;
     
            
        }
    }
}
