
namespace TutorCenter.Client;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        //services.AddMappings();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }
}

