using System.Text;
using System.Text.Json;
using System.Web;

namespace IoT_Architectures.Client.Core.Rest;

internal class HttpRequestMessageBuilder
{
    /// <summary>
    ///     The endpoint that the <see cref="HttpRequestMessage" /> will be using.
    /// </summary>
    private readonly string _endpoint;

    /// <summary>
    ///     A <see cref="Dictionary{TKey,TValue}" /> of <see cref="string" />, <see cref="string" /> holding the headers.
    /// </summary>
    private readonly Dictionary<string, string> _headers = new();

    /// <summary>
    ///     A <see cref="Dictionary{TKey,TValue}" /> of <see cref="string" />, <see cref="string" /> holding the query
    ///     parameters.
    /// </summary>
    private readonly Dictionary<string, string> _queryParameters = new();

    /// <summary>
    ///     The JSON content of the <see cref="HttpRequestMessage" />.
    /// </summary>
    private StringContent? _content;

    /// <summary>
    ///     The <see cref="HttpMethod" /> the <see cref="HttpRequestMessage" /> will be using.
    /// </summary>
    private HttpMethod _method;

    /// <summary>
    ///     Initializes a new instance of <see cref="HttpRequestMessageBuilder" />.
    /// </summary>
    /// <param name="endpoint">The endpoint of where the <see cref="HttpRequestMessage" /> will be send to.</param>
    internal HttpRequestMessageBuilder(string endpoint)
    {
        _endpoint = $"/api/v1{endpoint}";
        _method = HttpMethod.Get;
    }

    /// <summary>
    ///     Sets the <see cref="HttpMethod" /> of the request.
    /// </summary>
    /// <param name="method">The <see cref="HttpMethod" /> that will be used.</param>
    /// <returns>
    ///     The updated <see cref="HttpRequestMessageBuilder" />.
    /// </returns>
    internal HttpRequestMessageBuilder WithMethod(HttpMethod method)
    {
        _method = method;
        return this;
    }

    /// <summary>
    ///     Adds a query parameter to the <see cref="HttpRequestMessageBuilder" />.
    /// </summary>
    /// <param name="name">The name of the query.</param>
    /// <param name="value">The value of the query.</param>
    /// <returns>
    ///     The updated <see cref="HttpRequestMessageBuilder" />.
    /// </returns>
    internal HttpRequestMessageBuilder WithQueryParameter(string name, string value)
    {
        _queryParameters.Add(name, value);
        return this;
    }

    /// <summary>
    ///     Adds a header to the <see cref="HttpRequestMessageBuilder" />.
    /// </summary>
    /// <param name="name">The name of the header.</param>
    /// <param name="value">The value of the header.</param>
    /// <returns>
    ///     The updated <see cref="HttpRequestMessageBuilder" />.
    /// </returns>
    internal HttpRequestMessageBuilder WithHeader(string name, string? value)
    {
        if (value is null)
        {
            return this;
        }

        _headers.Add(name, value);
        return this;
    }

    /// <summary>
    ///     Adds header parameters to the <see cref="HttpRequestMessageBuilder" />.
    /// </summary>
    /// <param name="headers">The <see cref="IEnumerable{T}" /> containing the headers.</param>
    /// <returns>
    ///     The updated <see cref="HttpRequestMessageBuilder" />.
    /// </returns>
    internal HttpRequestMessageBuilder WithHeaders(IEnumerable<KeyValuePair<string, string>>? headers)
    {
        if (headers is null)
        {
            return this;
        }

        foreach (var header in headers)
        {
            WithHeader(header.Key, header.Value);
        }

        return this;
    }

    /// <summary>
    ///     Adds query parameters to the <see cref="HttpRequestMessageBuilder" />.
    /// </summary>
    /// <param name="queries">The <see cref="IEnumerable{T}" /> containing the queries.</param>
    /// <returns>
    ///     The updated <see cref="HttpRequestMessageBuilder" />.
    /// </returns>
    internal HttpRequestMessageBuilder WithQueryParameters(IEnumerable<KeyValuePair<string, string>>? queries)
    {
        if (queries is null)
        {
            return this;
        }

        foreach (var query in queries)
        {
            WithQueryParameter(query.Key, query.Value);
        }

        return this;
    }

    /// <summary>
    ///     Adds a JSON body to the <see cref="HttpRequestMessageBuilder" />.
    /// </summary>
    /// <param name="value">The value that will be added as JSON.</param>
    /// <typeparam name="TValue">The type of <paramref name="value" />.</typeparam>
    /// <returns>
    ///     The updated <see cref="HttpRequestMessageBuilder" />.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when the <see cref="_method" /> is <see cref="HttpMethod.Get" />.</exception>
    internal HttpRequestMessageBuilder WithBody<TValue>(TValue value) where TValue : notnull
    {
        if (_method == HttpMethod.Get)
        {
            throw new ArgumentException("Can not add a body to a GET request");
        }

        _content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return this;
    }

    /// <summary>
    ///     Builds the <see cref="HttpRequestMessageBuilder" /> into a <see cref="HttpRequestMessage" />.
    /// </summary>
    /// <returns>
    ///     The <see cref="HttpRequestMessage" />.
    /// </returns>
    internal HttpRequestMessage Build()
    {
        var queryParameters = HttpUtility.ParseQueryString(string.Empty);
        foreach (var queryParam in _queryParameters)
        {
            queryParameters.Add(queryParam.Key, queryParam.Value);
        }

        var request = new HttpRequestMessage(_method, _endpoint + (queryParameters.Count > 0 ? "?" + queryParameters : string.Empty))
        {
            Content = _content
        };

        foreach (var header in _headers)
        {
            request.Headers.Add(header.Key, header.Value);
        }

        return request;
    }
}