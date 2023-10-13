using System.Globalization;
using System.Reflection;
using Mediator;
using Microsoft.Extensions.Logging;

namespace IoT_Architectures.Api.Core.Endpoints.Version;

public class GetVersionQueryHandler : IRequestHandler<GetVersionQuery, VersionResponse>
{
    private readonly ILogger<GetVersionQueryHandler> _logger;

    public GetVersionQueryHandler(ILogger<GetVersionQueryHandler> logger)
    {
        _logger = logger;
    }

    public ValueTask<VersionResponse> Handle(GetVersionQuery request, CancellationToken cancellationToken)
    {
        var assembly = request.Assembly;
        var version = request.Assembly.GetName().Version;
        var buildDate = GetBuildDate(assembly);

        return ValueTask.FromResult(new VersionResponse($"v{version}", buildDate));
    }

    private DateTime GetBuildDate(Assembly assembly)
    {
        const string buildVersionMetadataPrefix = "+build";

        var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

        if (attribute?.InformationalVersion is null)
        {
            _logger.LogWarning("Failed to find InformationalVersion");
            return DateTime.UnixEpoch;
        }

        var value = attribute.InformationalVersion;
        var index = value.IndexOf(buildVersionMetadataPrefix, StringComparison.Ordinal);

        if (index <= 0)
        {
            _logger.LogWarning("InformationalVersion does not contain a date");
            return DateTime.UnixEpoch;
        }

        value = value[(index + buildVersionMetadataPrefix.Length)..];
        return DateTime.ParseExact(value, "yyyy-MM-ddTHH:mm:ss:fffZ", CultureInfo.InvariantCulture).ToUniversalTime();
    }
}