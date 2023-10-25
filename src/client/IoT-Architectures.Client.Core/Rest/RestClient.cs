using System.Data;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IoT_Architectures.Client.Core.Rest;

public class RestClient : IRestClient
{
    private readonly ILogger<RestClient> _logger;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly HttpClient _client;

    /// <summary>
    ///     Initializes a new instance of <see cref="RestClient" />.
    /// </summary>
    public RestClient(ILogger<RestClient> logger, IHttpClientFactory httpClientFactory, IOptions<JsonSerializerOptions> serializerOptions)
    {
        _logger = logger;
        _serializerOptions = serializerOptions.Value;
        _client = httpClientFactory.CreateClient();
        _client.BaseAddress = new Uri("https://release-webhook.brammys.com/");
    }

    /// <inheritdoc />
    public async Task<TEntity> GetAsync<TEntity>(string endpoint, IEnumerable<KeyValuePair<string, string>>? queries = null, CancellationToken ct = default)
        where TEntity : notnull
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithQueryParameters(queries)
            .WithMethod(HttpMethod.Get);

        return await SendRequestAsync<TEntity>(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TEntity> PostAsync<TEntity, TBody>(string endpoint, TBody body, string? auditLogReason = null, CancellationToken ct = default)
        where TEntity : notnull where TBody : notnull
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithMethod(HttpMethod.Post)
            .WithBody(body);

        return await SendRequestAsync<TEntity>(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task PostAsync<TBody>(string endpoint, TBody body, string? auditLogReason = null, CancellationToken ct = default) where TBody : notnull
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithMethod(HttpMethod.Post)
            .WithBody(body);

        await SendRequestAsync(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TEntity> PostAsync<TEntity>(string endpoint, string? auditLogReason = null, CancellationToken ct = default) where TEntity : notnull
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithMethod(HttpMethod.Post);

        return await SendRequestAsync<TEntity>(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TEntity> DeleteAsync<TEntity>(
        string endpoint,
        IEnumerable<KeyValuePair<string, string>>? queries = null,
        string? auditLogReason = null,
        CancellationToken ct = default
    ) where TEntity : notnull
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithQueryParameters(queries)
            .WithMethod(HttpMethod.Delete);

        return await SendRequestAsync<TEntity>(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(
        string endpoint,
        IEnumerable<KeyValuePair<string, string>>? queries = null,
        string? auditLogReason = null,
        CancellationToken ct = default
    )
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithQueryParameters(queries)
            .WithMethod(HttpMethod.Delete);

        await SendRequestAsync(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TEntity> PutAsync<TEntity, TBody>(string endpoint, TBody body, string? auditLogReason = null, CancellationToken ct = default)
        where TEntity : notnull where TBody : notnull
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithMethod(HttpMethod.Put)
            .WithBody(body);

        return await SendRequestAsync<TEntity>(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task PutAsync<TBody>(string endpoint, TBody body, string? auditLogReason = null, CancellationToken ct = default) where TBody : notnull
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithMethod(HttpMethod.Put)
            .WithBody(body);

        await SendRequestAsync(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task PutAsync(string endpoint, string? auditLogReason = null, CancellationToken ct = default)
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithMethod(HttpMethod.Put);

        await SendRequestAsync(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <summary>
    ///     Send a request with the discord http client.
    /// </summary>
    /// <param name="requestBuilder">The <see cref="HttpRequestMessageBuilder" /> containing the details for the request.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <typeparam name="TEntity">The entity type that will be used for deserialization.</typeparam>
    /// <returns>
    ///     The deserialized <typeparamref name="TEntity" />.
    /// </returns>
    private async Task<TEntity> SendRequestAsync<TEntity>(HttpRequestMessageBuilder requestBuilder, CancellationToken ct) where TEntity : notnull
    {
        using var request = requestBuilder.Build();
        using var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(await response.Content.ReadAsStringAsync(ct).ConfigureAwait(false));
        }

        Console.WriteLine(response.StatusCode);
        Console.WriteLine(await response.Content.ReadAsStringAsync().ConfigureAwait(false));

        return await DeserializeResponseAsync<TEntity>(response, ct).ConfigureAwait(false);
    }

    /// <summary>
    ///     Deserializes a json response.
    /// </summary>
    /// <param name="response">The <see cref="HttpResponseMessage" /> response.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <typeparam name="TEntity">The entity type that will be used for deserialization.</typeparam>
    /// <returns>
    ///     The deserialized <typeparamref name="TEntity" />.
    /// </returns>
    /// <exception cref="NoNullAllowedException">Thrown when <typeparamref name="TEntity" /> is null.</exception>
    private async Task<TEntity> DeserializeResponseAsync<TEntity>(HttpResponseMessage response, CancellationToken ct = default) where TEntity : notnull
    {
        if (response.IsSuccessStatusCode)
        {
            return (await JsonSerializer.DeserializeAsync<TEntity>(
                    await response.Content.ReadAsStreamAsync(ct).ConfigureAwait(false),
                    _serializerOptions,
                    ct
                )
                .ConfigureAwait(false))!;
        }

        if (response.Content.Headers.ContentLength <= 0)
        {
            _logger.LogError("An unknown HTTP error occured");
        }

        throw new HttpRequestException(await response.Content.ReadAsStringAsync(ct).ConfigureAwait(false));
    }

    /// <summary>
    ///     Send a request with the discord http client.
    /// </summary>
    /// <param name="requestBuilder">The <see cref="HttpRequestMessageBuilder" /> containing the details for the request.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    private async Task SendRequestAsync(HttpRequestMessageBuilder requestBuilder, CancellationToken ct)
    {
        try
        {
            using var request = requestBuilder.Build();
            using var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogError("Error: {Error}", e.ToString());
        }
    }
}