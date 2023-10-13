using IoT_Architectures.Api.Domain;
using IoT_Architectures.Client.Persistence.Mongodb;
using Microsoft.Extensions.DependencyInjection;

namespace IoT_Architectures.Api.Core;

public static class DependencyInjection
{
    public static IServiceCollection RegisterCore(this IServiceCollection services)
    {
        services.RegisterDomain();
        services.RegisterMongodb();

        return services;
    }
}