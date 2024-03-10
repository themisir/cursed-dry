namespace DryApi.Demo4;

public sealed class ValueChecker : IValueChecker
{
    private readonly string _otherValue;

    public ValueChecker(string otherValue)
    {
        _otherValue = otherValue;
    }

    public bool TestValue(string value)
    {
        return value == _otherValue;
    }
}