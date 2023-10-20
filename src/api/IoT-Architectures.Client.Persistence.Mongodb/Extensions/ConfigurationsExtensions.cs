using Microsoft.Extensions.Configuration;

namespace IoT_Architectures.Client.Persistence.Mongodb.Extensions;

public static class ConfigurationsExtensions
{
    public static string GetMongodbHost(this IConfiguration configuration)
    {
        const string key = "Mongodb:Host";
        return GetAndValidateKey(configuration, key);
    }

    public static string GetMongodbDatabase(this IConfiguration configuration)
    {
        const string key = "Mongodb:Database";
        return GetAndValidateKey(configuration, key);
    }

    public static string? GetMongodbUsername(this IConfiguration configuration)
    {
        const string key = "Mongodb:Username";
        return GetKey(configuration, key);
    }

    public static string? GetMongodbPassword(this IConfiguration configuration)
    {
        const string key = "Mongodb:Password";
        return GetKey(configuration, key);
    }

    public static bool? GetMongodbIsAtlas(this IConfiguration configuration)
    {
        const string key = "Mongodb:IsAtlas";
        return bool.TryParse(GetKey(configuration, key), out var isMongodbAtlas) ? isMongodbAtlas : null;
    }

    private static string GetAndValidateKey(IConfiguration configuration, string key)
    {
        return GetKey(configuration, key) ?? throw new ArgumentNullException(key, $"{key} is not set in the configuration file.");
    }

    private static string? GetKey(IConfiguration configuration, string key)
    {
        return configuration[key];
    }
}