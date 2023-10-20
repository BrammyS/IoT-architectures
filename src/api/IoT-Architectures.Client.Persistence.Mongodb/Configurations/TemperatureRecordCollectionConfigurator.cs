using IoT_Architectures.Client.Persistence.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace IoT_Architectures.Client.Persistence.Mongodb.Configurations;

public class TemperatureRecordCollectionConfigurator : ICollectionConfigurator
{
    /// <inheritdoc />
    public void ConfigureCollection()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(TemperatureRecord))) return;

        BsonClassMap.RegisterClassMap<TemperatureRecord>(
            cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.Latitude).SetElementName("Latitude").SetIsRequired(true);
                cm.MapMember(c => c.Longitude).SetElementName("Longitude").SetIsRequired(true);
                cm.MapMember(c => c.Temperature).SetElementName("Temperature").SetIsRequired(true);
                cm.MapMember(c => c.Timestamp).SetElementName("Timestamp").SetIsRequired(true);
            }
        );
    }
}
    
