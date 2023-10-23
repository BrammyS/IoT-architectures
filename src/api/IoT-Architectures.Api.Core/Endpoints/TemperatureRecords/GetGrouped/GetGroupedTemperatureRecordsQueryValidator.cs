using FluentValidation;

namespace IoT_Architectures.Api.Core.Endpoints.TemperatureRecords.GetGrouped;

public class GetGroupedTemperatureRecordsQueryValidator : AbstractValidator<GetGroupedTemperatureRecordsQuery>
{
    public GetGroupedTemperatureRecordsQueryValidator()
    {
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.Date).LessThan(DateTimeOffset.UtcNow);
    }
}