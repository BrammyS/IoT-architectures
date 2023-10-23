using IoT_Architectures.Client.Persistence.Repositories;
using IoT_Architectures.Client.Persistence.UnitOfWorks;

namespace IoT_Architectures.Client.Persistence.Mongodb.UnitOfWorks;

/// <inheritdoc />
public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(ITemperatureRecordRepository temperatureRecords)
    {
        TemperatureRecords = temperatureRecords;
    }
    
    /// <inheritdoc />
    public ITemperatureRecordRepository TemperatureRecords { get; }
}