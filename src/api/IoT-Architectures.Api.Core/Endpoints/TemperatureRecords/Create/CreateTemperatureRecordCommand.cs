using Mediator;

namespace IoT_Architectures.Api.Core.Endpoints.TemperatureRecords.Create;

public record CreateTemperatureRecordCommand(double Latitude, double Longitude, double Temperature, double UnixTime) : IRequest;