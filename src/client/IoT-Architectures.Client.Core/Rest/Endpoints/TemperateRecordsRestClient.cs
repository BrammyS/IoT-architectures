using IoT_Architectures.Client.Core.Rest.RestModels;

namespace IoT_Architectures.Client.Core.Rest.Endpoints;

public class TemperateRecordsRestClient
{
    private readonly IRestClient _restClient;

    public TemperateRecordsRestClient(IRestClient restClient)
    {
        _restClient = restClient;
    }

    public async Task<List<TemperatureRecord>> GetAllTemperatureRecordsAsync(CancellationToken ct = default)
    {
        const string endpoint = "/temperaturerecords";
        return await _restClient.GetAsync<List<TemperatureRecord>>(endpoint, ct: ct).ConfigureAwait(false);
    }

    public async Task<List<GroupedTemperatureRecord>> GetGroupedTemperatureRecordsAsync(DateTimeOffset date, int hours)
    {
        var endpoint = $"/temperaturerecords/grouped?date={date.ToUniversalTime():yyyy-MM-ddTHH:mm:ss.fffZ}&hours={hours}";
        return await _restClient.GetAsync<List<GroupedTemperatureRecord>>(endpoint).ConfigureAwait(false);
    }
}