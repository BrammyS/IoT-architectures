using System.Linq.Expressions;
using IoT_Architectures.Client.Persistence.Entities;
using IoT_Architectures.Client.Persistence.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace IoT_Architectures.Client.Persistence.Mongodb.Repositories;

/// <inheritdoc />
public class Repository<T> : IRepository<T> where T : BaseDocument
{
    protected readonly IMongoCollection<T> MongoCollection;

    public Repository(MongoContext context, string collectionName)
    {
        CollectionName = collectionName;
        MongoCollection = context.GetCollection<T>(CollectionName);
    }

    public string CollectionName { get; }

    /// <inheritdoc />
    public Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return MongoCollection.Find(predicate).FirstOrDefaultAsync()!;
    }

    /// <inheritdoc />
    public Task<List<T>> GetAllAsync()
    {
        return MongoCollection.Find(FilterDefinition<T>.Empty).ToListAsync();
    }

    /// <inheritdoc />
    public Task<List<T>> GetAllAsync(int page, int pageAmount)
    {
        return MongoCollection.Find(FilterDefinition<T>.Empty)
            .Skip((page - 1) * pageAmount)
            .Limit(pageAmount)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate)
    {
        return await MongoCollection.Find(predicate).ToListAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task AddAsync(T document)
    {
        if (string.IsNullOrEmpty(document.BsonObjectId)) document.BsonObjectId = ObjectId.GenerateNewId().ToString()!;

        return MongoCollection.InsertOneAsync(document);
    }

    /// <inheritdoc />
    public Task AddAsync(List<T> documents)
    {
        foreach (var document in documents.Where(document => string.IsNullOrEmpty(document.BsonObjectId)))
            document.BsonObjectId = ObjectId.GenerateNewId().ToString()!;

        return MongoCollection.InsertManyAsync(documents);
    }

    /// <inheritdoc />
    public async Task<long> RemoveAsync(string objectId)
    {
        var filter = Builders<T>.Filter.Eq(x => x.BsonObjectId, objectId);
        var result = await MongoCollection.DeleteOneAsync(filter).ConfigureAwait(false);
        return result.DeletedCount;
    }

    /// <inheritdoc />
    public async Task<long> RemoveAsync(IEnumerable<string> objectIds)
    {
        var filter = Builders<T>.Filter.In(x => x.BsonObjectId, objectIds);
        var result = await MongoCollection.DeleteManyAsync(filter).ConfigureAwait(false);
        return result.DeletedCount;
    }

    /// <inheritdoc />
    public async Task<long> RemoveWhereAsync(Expression<Func<T, bool>> predicateSearch)
    {
        var result = await MongoCollection.DeleteManyAsync(predicateSearch).ConfigureAwait(false);
        return result.DeletedCount;
    }

    /// <inheritdoc />
    public Task UpdateValueAsync<TSearchValue, TValue>(
        Expression<Func<T, TSearchValue>> predicateSearch,
        TSearchValue searchValue,
        Expression<Func<T, TValue?>> predicateNew,
        TValue? newValue
    )
    {
        var filter = Builders<T>.Filter.Eq(predicateSearch, searchValue);
        var update = Builders<T>.Update.Set(predicateNew, newValue);
        return MongoCollection.UpdateOneAsync(filter, update);
    }

    /// <inheritdoc />
    public Task UpdateValueAsync<TValue>(string objectId, Expression<Func<T, TValue?>> predicateNew, TValue? newValue)
    {
        var filter = Builders<T>.Filter.Eq(x => x.BsonObjectId, objectId);
        var update = Builders<T>.Update.Set(predicateNew, newValue);
        return MongoCollection.UpdateOneAsync(filter, update);
    }

    /// <inheritdoc />
    public Task IncrementValueAsync(
        string objectId,
        Expression<Func<T, long>> predicateIncrement,
        long incrementAmount = 1
    )
    {
        var filter = Builders<T>.Filter.Eq(e => e.BsonObjectId, objectId);
        var update = Builders<T>.Update.Inc(predicateIncrement, incrementAmount);
        return MongoCollection.UpdateOneAsync(filter, update);
    }

    /// <inheritdoc />
    public Task IncrementValueAsync<TSearchValue>(
        Expression<Func<T, TSearchValue>> predicateSearch,
        TSearchValue searchValue,
        Expression<Func<T, long>> predicateIncrement,
        long incrementAmount = 1
    )
    {
        var filter = Builders<T>.Filter.Eq(predicateSearch, searchValue);
        var update = Builders<T>.Update.Inc(predicateIncrement, incrementAmount);
        return MongoCollection.UpdateOneAsync(filter, update);
    }

    /// <inheritdoc />
    public async Task<List<T>> GetLastDocumentsAsync(int count)
    {
        var documents = await MongoCollection.AsQueryable()
            .OrderByDescending(x => x.AddedAtUtc)
            .Take(count)
            .ToListAsync()
            .ConfigureAwait(false);
        return new List<T>(documents.OrderBy(x => x.AddedAtUtc));
    }

    /// <inheritdoc />
    public Task<long> DocumentCountAsync()
    {
        return MongoCollection.EstimatedDocumentCountAsync();
    }

    /// <inheritdoc />
    public Task<long> CountWhereAsync(Expression<Func<T, bool>> predicate)
    {
        return MongoCollection.CountDocumentsAsync(predicate);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        var docCount = await MongoCollection.CountDocumentsAsync(predicate).ConfigureAwait(false);
        return docCount > 0;
    }

    /// <inheritdoc />
    public Task ReplaceOneAsync<TSearchValue>(Expression<Func<T, TSearchValue>> predicateSearch, TSearchValue searchValue, T newValue)
    {
        var filter = Builders<T>.Filter.Eq(predicateSearch, searchValue);
        return MongoCollection.ReplaceOneAsync(filter, newValue);
    }

    /// <inheritdoc />
    public Task ReplaceOneAsync(string objectId, T newValue)
    {
        var filter = Builders<T>.Filter.Eq(e => e.BsonObjectId, objectId);
        return MongoCollection.ReplaceOneAsync(filter, newValue);
    }

    /// <inheritdoc />
    public async Task<T?> GetRandomDocument()
    {
        var result = await MongoCollection.AsQueryable().Sample(1).FirstOrDefaultAsync().ConfigureAwait(false);
        return result;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<T>> GetRandomDocuments(uint amount)
    {
        var result = await MongoCollection.AsQueryable().Sample(amount).ToListAsync().ConfigureAwait(false);
        return result;
    }
}