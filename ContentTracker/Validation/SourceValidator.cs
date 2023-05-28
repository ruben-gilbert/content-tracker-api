using ContentTracker.Exceptions;
using ContentTracker.Models;
using FluentValidation;
using FluentValidation.Results;

namespace ContentTracker.Validation;

public class SourceValidator : AbstractValidator<Source>
{
    public SourceValidator()
    {
        RuleFor(source => source.SourceName).NotNull()!.MustBeKnownSourceName();
    }

    protected override void RaiseValidationException(
        ValidationContext<Source> context,
        ValidationResult result
    )
    {
        ValidationException e = new ValidationException(result.Errors);
        throw new InvalidSourceException(e.Message, e);
    }
}
