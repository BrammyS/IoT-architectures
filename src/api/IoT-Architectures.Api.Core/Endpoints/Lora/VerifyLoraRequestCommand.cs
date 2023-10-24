using Mediator;

namespace IoT_Architectures.Api.Core.Endpoints.Lora;

public record VerifyLoraRequestCommand(Stream RequestBody, string Token) : IRequest<bool>;