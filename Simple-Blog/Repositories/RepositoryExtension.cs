namespace Simple_BlogApi.Repositories
{
    public class RepositoryExtension
    {
        public static void AddRepositiories(IServiceCollection services, bool isIncluideIdentityService)
        {
            services.AddScoped<IBlogRepository, BlogRepository>();
        }
    }
}
