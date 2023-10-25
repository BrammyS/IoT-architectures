namespace IoT_Architectures.Client.Domain.Models;

public record TemperatureRecord(double Latitude, double Longitude, double Temperature, DateTimeOffset Timestamp);