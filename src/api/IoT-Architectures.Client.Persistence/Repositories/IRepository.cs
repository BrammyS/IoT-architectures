using System.Linq.Expressions;
using IoT_Architectures.Client.Persistence.Entities;

namespace IoT_Architectures.Client.Persistence.Repositories;

/// <summary>
///     This is a generic Repository that should be used in all table/collection specific Repositories.
/// </summary>
/// <typeparam name="T">The object type of the table/collection</typeparam>
public interface IRepository<T> where T : BaseDocument
{
    /// <summary>
    ///     The collection name of the collection where the documents will be written to.
    /// </summary>
    string CollectionName { get; }

    /// <summary>
    ///     Get all rows/documents from the table/collection.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous get operation.
    ///     The task result contains the requested <list type="T"></list>"/>.
    /// </returns>
    Task<List<T>> GetAllAsync();

    /// <summary>
    ///     Get a paged list of rows/documents from the table/collection.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="pageAmount">The amount of objects per page.</param>
    /// <returns>
    ///     A task that represents the asynchronous get operation.
    ///     The task result contains the requested <list type="T"></list>"/>.
    /// </returns>
    Task<List<T>> GetAllAsync(int page, int pageAmount);

    /// <summary>
    ///     Find a rows/documents in the table/collection.
    /// </summary>
    /// <param name="predicate">The <see cref="Expression" /> that will be used to look for the row/document.</param>
    /// <returns>
    ///     A task that represents the asynchronous find operation.
    ///     The task result contains the requested <see cref="T" />.
    /// </returns>
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Get a list of objects where the <paramref name="predicate" /> is true.
    /// </summary>
    /// <param name="predicate">The <see cref="Expression" /> that will be used to look for the row/document.</param>
    /// <returns>
    ///     A task that represents the asynchronous where operation.
    ///     The task result contains the requested <list type="T"></list>.
    /// </returns>
    Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Add a row/document to the table/collection.
    /// </summary>
    /// <param name="document">The row/document that will be added to the table/collection.</param>
    /// <returns>
    ///     A task that represents the asynchronous add operation.
    /// </returns>
    Task AddAsync(T document);

    /// <summary>
    ///     Add a range of rows/documents to the table/collection.
    /// </summary>
    /// <param name="documents">The <list type="T"></list> that will be added to the table/collection.</param>
    /// <returns>
    ///     A task that represents the asynchronous add operation.
    /// </returns>
    Task AddAsync(List<T> documents);

    /// <summary>
    ///     Remove a row/document from the table/collection.
    /// </summary>
    /// <param name="objectId">The object id of the object you want to remove.</param>
    /// <returns>
    ///     A task that represents the asynchronous delete operation.
    /// </returns>
    Task<long> RemoveAsync(string objectId);

    /// <summary>
    ///     Remove a range of rows/documents from the table/collection.
    /// </summary>
    /// <param name="objectIds">The list of object ids of the objects you want to remove.</param>
    /// <returns>
    ///     The amount of deleted documents.
    ///     A task that represents the asynchronous delete operation.
    /// </returns>
    Task<long> RemoveAsync(IEnumerable<string> objectIds);

    /// <summary>
    ///     Remove rows/documents from the table/collection.
    /// </summary>
    /// <param name="predicateSearch">The <see cref="Expression" /> that will be used to look for the rows/documents.</param>
    /// <returns>
    ///     The amount of deleted documents.
    ///     A task that represents the asynchronous delete operation.
    /// </returns>
    Task<long> RemoveWhereAsync(Expression<Func<T, bool>> predicateSearch);

    /// <summary>
    ///     Update a value of a row/document in a table/collection.
    /// </summary>
    /// <param name="predicateSearch">The <see cref="Expression" /> that will be used to look for the row/document.</param>
    /// <param name="searchValue">The value that should match the <paramref name="predicateSearch" /> value.</param>
    /// <param name="predicateNew">
    ///     The <see cref="Expression" /> that will select which variable of the row/document will be
    ///     updated.
    /// </param>
    /// <param name="newValue">The value that will be set to the <paramref name="predicateNew" /> value.</param>
    /// <typeparam name="TSearchValue">The type of the search value.</typeparam>
    /// <typeparam name="TValue">The type of the value that will be updated.</typeparam>
    Task UpdateValueAsync<TSearchValue, TValue>(
        Expression<Func<T, TSearchValue>> predicateSearch,
        TSearchValue searchValue,
        Expression<Func<T, TValue?>> predicateNew,
        TValue? newValue
    );

    /// <summary>
    ///     Update a value of a row/document in a table/collection.
    /// </summary>
    /// <param name="objectId">The object id of the object that will be updated.</param>
    /// <param name="predicateNew">
    ///     The <see cref="Expression" /> that will select which variable of the row/document will be
    ///     updated.
    /// </param>
    /// <param name="newValue">The value that will be set to the <paramref name="predicateNew" /> value.</param>
    /// <typeparam name="TValue">The type of the value that will be updated.</typeparam>
    Task UpdateValueAsync<TValue>(string objectId, Expression<Func<T, TValue?>> predicateNew, TValue? newValue);

    /// <summary>
    ///     Increment a value of a row/document in a table/collection.
    /// </summary>
    /// <param name="objectId">The object id of the object that will be Incremented.</param>
    /// <param name="predicateIncrement">
    ///     The <see cref="Expression" /> that will select which variable of the row/document will
    ///     be Incremented.
    /// </param>
    /// <param name="incrementAmount">The amount that will be incremented to the new number.</param>
    Task IncrementValueAsync(
        string objectId,
        Expression<Func<T, long>> predicateIncrement,
        long incrementAmount = 1
    );

    /// <summary>
    ///     Increment a value of a row/document in a table/collection.
    /// </summary>
    /// <param name="predicateSearch">The <see cref="Expression" /> that will be used to look for the row/document.</param>
    /// <param name="searchValue">The value that should match the <paramref name="predicateSearch" /> value.</param>
    /// <param name="predicateIncrement">
    ///     The <see cref="Expression" /> that will select which variable of the row/document will
    ///     be Incremented.
    /// </param>
    /// <param name="incrementAmount">The amount that will be incremented to the new number.</param>
    /// <typeparam name="TSearchValue">The type of the search value.</typeparam>
    Task IncrementValueAsync<TSearchValue>(
        Expression<Func<T, TSearchValue>> predicateSearch,
        TSearchValue searchValue,
        Expression<Func<T, long>> predicateIncrement,
        long incrementAmount = 1
    );

    /// <summary>
    ///     Get the last documents in a collection.
    /// </summary>
    /// <param name="count">The amount of documents you want to get.</param>
    /// <returns>
    ///     A task that represents the asynchronous delete operation.
    ///     The task result contains a list of the requested documents.
    /// </returns>
    Task<List<T>> GetLastDocumentsAsync(int count);

    /// <summary>
    ///     Get the document count of the collection.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous delete operation.
    ///     The task result contains the document count.
    /// </returns>
    Task<long> DocumentCountAsync();

    /// <summary>
    ///     Get the document count where the <paramref name="predicate" /> is true.
    /// </summary>
    /// <param name="predicate">The <see cref="Expression" /> that will be used to look for the row/document.</param>
    /// <returns>
    ///     A task that represents the asynchronous delete operation.
    ///     The task result contains the document count.
    /// </returns>
    Task<long> CountWhereAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Checks if a document exists in the collection.
    /// </summary>
    /// <param name="predicate">The <see cref="Expression" /> that will be used to look for the row/document.</param>
    /// <returns>
    ///     A task that represents the asynchronous delete operation.
    ///     The task result contains whether or not the document exists.
    /// </returns>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Updates a row/document in the collection.
    /// </summary>
    /// <param name="predicateSearch">The <see cref="Expression" /> that will be used to look for the row/document.</param>
    /// <param name="searchValue">The value that should match the <paramref name="predicateSearch" /> value.</param>
    /// <param name="newValue">The value that will replace the old row/document.</param>
    /// <returns>
    ///     A task that represents the asynchronous ReplaceOneAsync operation.
    /// </returns>
    /// <typeparam name="TSearchValue">The type of the search value.</typeparam>
    Task ReplaceOneAsync<TSearchValue>(Expression<Func<T, TSearchValue>> predicateSearch, TSearchValue searchValue, T newValue);

    /// <summary>
    ///     Updates a row/document in the collection.
    /// </summary>
    /// <param name="objectId">The object id of the object that will be updated.</param>
    /// <param name="newValue">The value that will replace the old row/document.</param>
    /// <returns>
    ///     A task that represents the asynchronous ReplaceOneAsync operation.
    /// </returns>
    Task ReplaceOneAsync(string objectId, T newValue);

    /// <summary>
    ///     Get a random document form the database.
    /// </summary>
    /// <returns>
    ///     A random document or null when none was found.
    /// </returns>
    Task<T?> GetRandomDocument();

    /// <summary>
    ///     Get a <see cref="IEnumerable{T}" /> of random documents form the database.
    /// </summary>
    /// <returns>
    ///     A <see cref="IEnumerable{T}" /> of random documents or null when none was found.
    /// </returns>
    Task<IEnumerable<T>> GetRandomDocuments(uint amount);
}