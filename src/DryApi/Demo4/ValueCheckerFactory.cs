namespace DryApi.Demo4;

public sealed class ValueCheckerFactory
{
    public IValueChecker CreateChecker(string otherValue)
    {
        return new ValueChecker(otherValue);
    }
}