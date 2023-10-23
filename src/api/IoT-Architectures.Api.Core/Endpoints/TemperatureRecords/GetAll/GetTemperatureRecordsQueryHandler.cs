using IoT_Architectures.Client.Persistence.Entities;
using IoT_Architectures.Client.Persistence.UnitOfWorks;
using Mediator;
using Microsoft.Extensions.Logging;

namespace IoT_Architectures.Api.Core.Endpoints.TemperatureRecords.GetAll;

public class GetTemperatureRecordsQueryHandler : IRequestHandler<GetTemperatureRecordsQuery, IEnumerable<TemperatureRecord>>
{
    private readonly ILogger<GetTemperatureRecordsQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetTemperatureRecordsQueryHandler(ILogger<GetTemperatureRecordsQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async ValueTask<IEnumerable<TemperatureRecord>> Handle(GetTemperatureRecordsQuery request, CancellationToken cancellationToken)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Loading all temperature records from database");
        }
        
        return await _unitOfWork.TemperatureRecords.GetAllAsync().ConfigureAwait(false);
    }
}