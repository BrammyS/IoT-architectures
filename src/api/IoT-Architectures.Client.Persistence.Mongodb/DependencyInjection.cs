using IoT_Architectures.Client.Persistence.Mongodb.Configurations;
using IoT_Architectures.Client.Persistence.Mongodb.Repositories;
using IoT_Architectures.Client.Persistence.Mongodb.UnitOfWorks;
using IoT_Architectures.Client.Persistence.Repositories;
using IoT_Architectures.Client.Persistence.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace IoT_Architectures.Client.Persistence.Mongodb;

public static class DependencyInjection
{
    public static IServiceCollection RegisterMongodb(this IServiceCollection services)
    {
        services.AddSingleton<MongoContext>();
        services.AddSingleton<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<ITemperatureRecordRepository, TemperatureRecordRepository>();

        services.Scan(
            scan => scan
                .FromAssemblyOf<ICollectionConfigurator>()
                .AddClasses(classes => classes.AssignableTo<ICollectionConfigurator>())
                .AsImplementedInterfaces()
                .WithTransientLifetime()
        );

        return services;
    }
}