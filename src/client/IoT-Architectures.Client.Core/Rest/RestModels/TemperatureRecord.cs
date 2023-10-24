namespace IoT_Architectures.Client.Core.Rest.RestModels;

public record TemperatureRecord(double Latitude, double Longitude, double Temperature, DateTimeOffset Timestamp);