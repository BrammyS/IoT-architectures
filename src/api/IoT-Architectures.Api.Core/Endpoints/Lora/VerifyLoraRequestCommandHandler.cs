using System.Security.Cryptography;
using System.Text;
using Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IoT_Architectures.Api.Core.Endpoints.Lora;

public class VerifyLoraRequestCommandHandler : IRequestHandler<VerifyLoraRequestCommand, bool>
{
    private readonly ILogger<VerifyLoraRequestCommandHandler> _logger;
    private readonly string? _secret;

    public VerifyLoraRequestCommandHandler(ILogger<VerifyLoraRequestCommandHandler> logger, IConfiguration configuration)
    {
        _logger = logger;
        _secret = configuration["SharedSecret"];
    }

    public async ValueTask<bool> Handle(VerifyLoraRequestCommand request, CancellationToken cancellationToken)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
            _logger.LogDebug("Validating lora request, token: {Token}", request.Token);
        
        var bodyString = await ReadRequestBodyAsync(request.RequestBody).ConfigureAwait(false);
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(bodyString + _secret));
        
        var token = Convert.ToHexString(hashBytes);
        var validateResult = token.Equals(request.Token, StringComparison.OrdinalIgnoreCase);
        if(!validateResult)
            _logger.LogWarning("Failed to validate lora request, token: {Token}, expected token: {ExpectedToken}", request.Token, token);
            
        return validateResult;
    }

    private static async Task<string> ReadRequestBodyAsync(Stream requestBody)
    {
        using var reader = new StreamReader(requestBody, Encoding.UTF8);
        return await reader.ReadToEndAsync().ConfigureAwait(false);
    }
}