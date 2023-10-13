using IoT_Architectures.Client.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace IoT_Architectures.Client.Core;

public static class DependencyInjection
{
    public static IServiceCollection RegisterCore(this IServiceCollection services)
    {
        services.RegisterDomain();
        return services;
    }
}