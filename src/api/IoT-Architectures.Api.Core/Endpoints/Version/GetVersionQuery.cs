using System.Reflection;
using Mediator;

namespace IoT_Architectures.Api.Core.Endpoints.Version;

public class GetVersionQuery : IRequest<VersionResponse>
{
    public GetVersionQuery(Assembly assembly)
    {
        Assembly = assembly;
    }

    public Assembly Assembly { get; init; }
}