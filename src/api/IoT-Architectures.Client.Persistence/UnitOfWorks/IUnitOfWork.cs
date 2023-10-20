using IoT_Architectures.Client.Persistence.Entities;
using IoT_Architectures.Client.Persistence.Repositories;

namespace IoT_Architectures.Client.Persistence.UnitOfWorks;

/// <summary>
///     This UnitOfWork contains all the Repositories used to query the all the tables/collections.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     The <see cref="ITemperatureRecordRepository" /> used to query the <see cref="TemperatureRecord"/> collection.
    /// </summary>
    ITemperatureRecordRepository TemperatureRecords { get; }
}