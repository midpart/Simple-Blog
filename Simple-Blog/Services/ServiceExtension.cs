using Simple_Blog.Repositories;

namespace Simple_Blog.Services
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services, bool isIncluideIdentityService)
        {
            //register all repositories
            RepositoryExtension.AddRepositiories(services, isIncluideIdentityService);

            services.AddScoped<IBlogService, BlogService>();
        }
    }
}
