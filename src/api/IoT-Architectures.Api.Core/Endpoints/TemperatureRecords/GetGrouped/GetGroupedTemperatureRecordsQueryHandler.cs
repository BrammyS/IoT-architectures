using IoT_Architectures.Api.Core.Endpoints.TemperatureRecords.GetAll;
using IoT_Architectures.Client.Persistence.Entities;
using IoT_Architectures.Client.Persistence.UnitOfWorks;
using Mediator;
using Microsoft.Extensions.Logging;

namespace IoT_Architectures.Api.Core.Endpoints.TemperatureRecords.GetGrouped;

public class GetGroupedTemperatureRecordsQueryHandler : IRequestHandler<GetGroupedTemperatureRecordsQuery, IEnumerable<GroupedTemperatureRecordResponse>>
{
    private readonly ILogger<GetGroupedTemperatureRecordsQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetGroupedTemperatureRecordsQueryHandler(ILogger<GetGroupedTemperatureRecordsQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async ValueTask<IEnumerable<GroupedTemperatureRecordResponse>> Handle(GetGroupedTemperatureRecordsQuery request, CancellationToken cancellationToken)
    {
        if (_logger.IsEnabled(LogLevel.Debug)) _logger.LogDebug("Loading all temperature records from database grouped by location");

        var hours = (double)request.Hours / 2;
        var temperatureRecords = await _unitOfWork.TemperatureRecords
            .WhereAsync(x => x.Timestamp > request.Date.AddHours(-hours) && x.Timestamp < request.Date.AddHours(hours))
            .ConfigureAwait(false);

        return CalculateAverageTemperaturePerGrid(temperatureRecords);
    }

    private static IEnumerable<GroupedTemperatureRecordResponse> CalculateAverageTemperaturePerGrid(IEnumerable<TemperatureRecord> records)
    {
        return records.GroupBy(record => CalculateGridIdentifier(record.Latitude, record.Longitude))
            .Select(group => new GroupedTemperatureRecordResponse(group.Key.Latitude, group.Key.Longitude, group.Average(record => record.Temperature)))
            .ToList();
    }

    private static (double Latitude, double Longitude) CalculateGridIdentifier(double latitude, double longitude)
    {
        // 2 decimal places is approximately 1.11km
        var roundedLatitude = Math.Round(latitude, 2);
        var roundedLongitude = Math.Round(longitude, 2);

        return (roundedLatitude, roundedLongitude);
    }
}