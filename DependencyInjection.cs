using Microsoft.Extensions.DependencyInjection;

namespace Nlabs.FileSrevice;
public static class DependencyInjection
{
    public static IServiceCollection AddFileService
        (
            this IServiceCollection services,
            string webRootPath
        )
    {

        var fileHostDependecyInjection = new FileHostEnvironment
        {
            WebRootPath = webRootPath
        };

        services.AddSingleton<IFileHostEnvironment>(fileHostDependecyInjection);

        return services;
    }
}
