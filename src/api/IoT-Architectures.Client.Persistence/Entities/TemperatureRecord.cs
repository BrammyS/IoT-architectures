namespace IoT_Architectures.Client.Persistence.Entities;

public class TemperatureRecord : BaseDocument
{
    public TemperatureRecord(double latitude, double longitude, double temperature, DateTimeOffset timestamp)
    {
        Latitude = latitude;
        Longitude = longitude;
        Temperature = temperature;
        Timestamp = timestamp;
    }

    public double Latitude { get; }
    public double Longitude { get; }
    public double Temperature { get; }
    public DateTimeOffset Timestamp { get; }
    
    public override string ToString()
    {
        return $"{nameof(Latitude)}: {Latitude}, {nameof(Longitude)}: {Longitude}, {nameof(Temperature)}: {Temperature}, {nameof(Timestamp)}: {Timestamp}";
    }
}