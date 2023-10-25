using System.Text.Json;
using System.Text.Json.Serialization;

namespace IoT_Architectures.Client.Core.Extensions;

public static class JsonSerializerOptionsExtensions
{
    public static JsonSerializerOptions RegisterJsonOptions(this JsonSerializerOptions options)
    {
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        return options;
    }
}