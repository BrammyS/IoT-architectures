using System.Diagnostics;
using Mediator;
using Microsoft.Extensions.Logging;

namespace IoT_Architectures.Api.Core.Common.Pipelines;

public class PerformancePipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<PerformancePipelineBehaviour<TRequest, TResponse>> _logger;

    public PerformancePipelineBehaviour(ILogger<PerformancePipelineBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async ValueTask<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TRequest, TResponse> next
    )
    {
        var startTime = Stopwatch.GetTimestamp();
        var response = await next(request, cancellationToken).ConfigureAwait(false);
        var elapsedTime = Stopwatch.GetElapsedTime(startTime);

        if (elapsedTime.TotalMilliseconds > 1500)
            _logger.LogWarning(
                "IoT.Architectures.Api Long Running Request: {Name} ({ElapsedTime} milliseconds) {Request}",
                typeof(TRequest).Name,
                elapsedTime.TotalMilliseconds.ToString("F"),
                request
            );

        return response;
    }
}