namespace IoT_Architectures.Client.Persistence.Entities;

public class TemperatureRecord : BaseDocument
{
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public required double Temperature { get; init; }
    public required DateTimeOffset Timestamp { get; init; }
    
    public override string ToString()
    {
        return $"{nameof(Latitude)}: {Latitude}, {nameof(Longitude)}: {Longitude}, {nameof(Temperature)}: {Temperature}, {nameof(Timestamp)}: {Timestamp}";
    }
}