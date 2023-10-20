using IoT_Architectures.Client.Persistence.Entities;
using Mediator;

namespace IoT_Architectures.Api.Core.Endpoints.TemperatureRecords;

public record GetTemperatureRecordsQuery : IRequest<IEnumerable<TemperatureRecord>>;