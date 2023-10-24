using IoT_Architectures.Api.Core.Endpoints.TemperatureRecords.GetAll;
using Mediator;

namespace IoT_Architectures.Api.Core.Endpoints.TemperatureRecords.GetGrouped;

public record GetGroupedTemperatureRecordsQuery(DateTimeOffset Date, int Hours) : IRequest<IEnumerable<GroupedTemperatureRecordResponse>>;