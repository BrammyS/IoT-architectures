using Mediator;

namespace IoT_Architectures.Api.Core.Endpoints.Lora.Webhook.Temperature;

public record LoraTemperatureReadingCommand(double Latitude, double Longitude, double Temperature, double UnixTime) : IRequest;
