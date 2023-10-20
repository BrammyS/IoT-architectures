using IoT_Architectures.Api.Domain.Models;
using Mediator;

namespace IoT_Architectures.Api.Core.Endpoints.Lora.Webhook;

public record LoraWebhookCommand(IReadOnlyList<SenML> Data) : IRequest;