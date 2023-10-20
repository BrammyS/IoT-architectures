using IoT_Architectures.Client.Persistence.Entities;
using IoT_Architectures.Client.Persistence.Repositories;

namespace IoT_Architectures.Client.Persistence.Mongodb.Repositories;

public class TemperatureRecordRepository : Repository<TemperatureRecord>, ITemperatureRecordRepository
{
    public TemperatureRecordRepository(MongoContext context) : base(context, nameof(TemperatureRecord) + "s")
    {
    }
}