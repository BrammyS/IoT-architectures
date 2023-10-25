using IoT_Architectures.Client.Persistence.Mongodb.Configurations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace IoT_Architectures.Client.Persistence.Mongodb;

public class BaseMongoContext
{
    /// <summary>
    ///     Creates a new <see cref="BaseMongoContext" />.
    /// </summary>
    protected BaseMongoContext(IEnumerable<ICollectionConfigurator> collectionConfigurators)
    {
        foreach (var collectionConfigurator in collectionConfigurators)
        {
            collectionConfigurator.ConfigureCollection();
        }

        // Set up MongoDB conventions
        var pack = new ConventionPack
        {
            new EnumRepresentationConvention(BsonType.String)
        };

        ConventionRegistry.Register("EnumStringConvention", pack, _ => true);
    }
}