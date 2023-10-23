using System.Reflection;
using FluentValidation;
using IoT_Architectures.Api.Core.Common.Pipelines;
using IoT_Architectures.Api.Domain;
using IoT_Architectures.Client.Persistence.Mongodb;
using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace IoT_Architectures.Api.Core;

public static class DependencyInjection
{
    public static IServiceCollection RegisterCore(this IServiceCollection services)
    {
        services.RegisterDomain();
        services.RegisterMongodb();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediator(
            options =>
            {
                options.Namespace = null;
                options.ServiceLifetime = ServiceLifetime.Transient;
            }
        );

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformancePipelineBehaviour<,>)); // 1st
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>)); // 2nd
        
        return services;
    }
}