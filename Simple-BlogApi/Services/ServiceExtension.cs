using Simple_BlogApi.Repositories;

namespace Simple_BlogApi.Services
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
