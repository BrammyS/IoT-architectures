using IoT_Architectures.Client.Core.Rest;
using IoT_Architectures.Client.Core.Rest.Endpoints;
using IoT_Architectures.Client.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace IoT_Architectures.Client.Core;

public static class DependencyInjection
{
    public static IServiceCollection RegisterCore(this IServiceCollection services)
    {
        services.RegisterDomain();
        services.AddHttpClient();

        services.AddTransient<IRestClient, RestClient>();
        services.AddTransient<TemperateRecordsRestClient>();

        return services;
    }
}