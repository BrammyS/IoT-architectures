using System.Text;
using IoT_Architectures.Client.Persistence.Mongodb.Extensions;
using Microsoft.Extensions.Configuration;

namespace IoT_Architectures.Client.Persistence.Mongodb.Builders;

public class MongodbConnectionStringBuilder
{
    private static string? _host;
    private static string? _database;
    private static string? _username;
    private static string? _password;

    public MongodbConnectionStringBuilder FromConfiguration(IConfiguration configuration)
    {
        _host = configuration.GetMongodbHost();
        _database = configuration.GetMongodbDatabase();
        _username = configuration.GetMongodbUsername();
        _password = configuration.GetMongodbPassword();
        
        return this;
    }

    public string Build()
    {
        if (string.IsNullOrWhiteSpace(_host)) throw new ArgumentNullException(_host);
        if (string.IsNullOrWhiteSpace(_database)) throw new ArgumentNullException(_database);

        var csBuilder = new StringBuilder();
        csBuilder.Append("mongodb+srv://");

        if (!string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(_password))
            csBuilder.Append($"{_username}:{_password}@");

        csBuilder.Append($"{_host}");
        csBuilder.Append("?retryWrites=true&w=majority&compressors=zlib,zstd&maxPoolSize=1024");

        return csBuilder.ToString();
    }
}