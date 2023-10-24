namespace IoT_Architectures.Client.Core.Rest;

public interface IRestClient
{
    /// <summary>
    ///     Send a <see cref="HttpMethod.Get" /> request to the <paramref name="endpoint" />.
    /// </summary>
    /// <param name="endpoint">The endpoint to where the request will be send to.</param>
    /// <param name="queries">The query parameters that will be included in the request.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <typeparam name="TEntity">The entity type that will be used for deserialization.</typeparam>
    /// <returns>
    ///     The deserialized <typeparamref name="TEntity" />.
    /// </returns>
    Task<TEntity> GetAsync<TEntity>(string endpoint, IEnumerable<KeyValuePair<string, string>>? queries = null, CancellationToken ct = default)
        where TEntity : notnull;

    /// <summary>
    ///     Send a <see cref="HttpMethod.Post" /> request to the <paramref name="endpoint" />.
    /// </summary>
    /// <param name="endpoint">The endpoint to where the request will be send to.</param>
    /// <param name="body">The body that will be send with the request.</param>
    /// <param name="auditLogReason">The reason for this action that will be set in the audit logs.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <typeparam name="TEntity">The entity type that will be used for deserialization.</typeparam>
    /// <typeparam name="TBody">The entity type that will be used to serialize the <paramref name="body" />.</typeparam>
    /// <returns>
    ///     The deserialized <typeparamref name="TEntity" />.
    /// </returns>
    Task<TEntity> PostAsync<TEntity, TBody>(string endpoint, TBody body, string? auditLogReason = null, CancellationToken ct = default)
        where TEntity : notnull where TBody : notnull;

    /// <summary>
    ///     Send a <see cref="HttpMethod.Post" /> request to the <paramref name="endpoint" />.
    /// </summary>
    /// <param name="endpoint">The endpoint to where the request will be send to.</param>
    /// <param name="body">The body that will be send with the request.</param>
    /// <param name="auditLogReason">The reason for this action that will be set in the audit logs.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <typeparam name="TBody">The entity type that will be used to serialize the <paramref name="body" />.</typeparam>
    Task PostAsync<TBody>(string endpoint, TBody body, string? auditLogReason = null, CancellationToken ct = default) where TBody : notnull;

    /// <summary>
    ///     Send a <see cref="HttpMethod.Post" /> request to the <paramref name="endpoint" />.
    /// </summary>
    /// <param name="endpoint">The endpoint to where the request will be send to.</param>
    /// <param name="auditLogReason">The reason for this action that will be set in the audit logs.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <typeparam name="TEntity">The entity type that will be used for deserialization.</typeparam>
    /// <returns>
    ///     The deserialized <typeparamref name="TEntity" />.
    /// </returns>
    Task<TEntity> PostAsync<TEntity>(string endpoint, string? auditLogReason = null, CancellationToken ct = default) where TEntity : notnull;

    /// <summary>
    ///     Send a <see cref="HttpMethod.Delete" /> request to the <paramref name="endpoint" />.
    /// </summary>
    /// <param name="endpoint">The endpoint to where the request will be send to.</param>
    /// <param name="queries">The query parameters that will be included in the request.</param>
    /// <param name="auditLogReason">The reason for this action that will be set in the audit logs.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <typeparam name="TEntity">The entity type that will be used for deserialization.</typeparam>
    /// <returns>
    ///     The deserialized <typeparamref name="TEntity" />.
    /// </returns>
    Task<TEntity> DeleteAsync<TEntity>(
        string endpoint,
        IEnumerable<KeyValuePair<string, string>>? queries = null,
        string? auditLogReason = null,
        CancellationToken ct = default
    )
        where TEntity : notnull;

    /// <summary>
    ///     Send a <see cref="HttpMethod.Delete" /> request to the <paramref name="endpoint" />.
    /// </summary>
    /// <param name="endpoint">The endpoint to where the request will be send to.</param>
    /// <param name="queries">The query parameters that will be included in the request.</param>
    /// <param name="auditLogReason">The reason for this action that will be set in the audit logs.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    Task DeleteAsync(string endpoint, IEnumerable<KeyValuePair<string, string>>? queries = null, string? auditLogReason = null, CancellationToken ct = default);

    /// <summary>
    ///     Send a <see cref="HttpMethod.Put" /> request to the <paramref name="endpoint" />.
    /// </summary>
    /// <param name="endpoint">The endpoint to where the request will be send to.</param>
    /// <param name="body">The body that will be send with the request.</param>
    /// <param name="auditLogReason">The reason for this action that will be set in the audit logs.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <typeparam name="TEntity">The entity type that will be used for deserialization.</typeparam>
    /// <typeparam name="TBody">The entity type that will be used to serialize the <paramref name="body" />.</typeparam>
    /// <returns>
    ///     The deserialized <typeparamref name="TEntity" />.
    /// </returns>
    Task<TEntity> PutAsync<TEntity, TBody>(string endpoint, TBody body, string? auditLogReason = null, CancellationToken ct = default)
        where TEntity : notnull where TBody : notnull;

    /// <summary>
    ///     Send a <see cref="HttpMethod.Put" /> request to the <paramref name="endpoint" />.
    /// </summary>
    /// <param name="endpoint">The endpoint to where the request will be send to.</param>
    /// <param name="body">The body that will be send with the request.</param>
    /// <param name="auditLogReason">The reason for this action that will be set in the audit logs.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <typeparam name="TBody">The entity type that will be used to serialize the <paramref name="body" />.</typeparam>
    Task PutAsync<TBody>(string endpoint, TBody body, string? auditLogReason = null, CancellationToken ct = default) where TBody : notnull;

    /// <summary>
    ///     Send a <see cref="HttpMethod.Put" /> request to the <paramref name="endpoint" />.
    /// </summary>
    /// <param name="endpoint">The endpoint to where the request will be send to.</param>
    /// <param name="auditLogReason">The reason for this action that will be set in the audit logs.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    Task PutAsync(string endpoint, string? auditLogReason = null, CancellationToken ct = default);
}