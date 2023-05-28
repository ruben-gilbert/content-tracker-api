using System.Reflection;
using ContentTracker.Clients;
using FluentValidation;

namespace ContentTracker.Validation;

public static class ValidationExtensions
{
    public static bool IsKnownSourceName(string name)
    {
        FieldInfo[] fields = typeof(Sources).GetFields(BindingFlags.Static | BindingFlags.Public);
        return fields.Any(f => f.GetValue(null)?.ToString() == name);
    }

    public static IRuleBuilderOptions<T, string> MustBeKnownSourceName<T>(
        this IRuleBuilder<T, string> rule
    )
    {
        return rule.Must(sourceName => IsKnownSourceName(sourceName))
            .WithMessage("{PropertyValue} is not a valid Source");
    }
}
