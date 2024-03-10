namespace DryApi.Demo3;

public sealed class NonEmptyValueChecker : IValueChecker
{
    public bool TestValue(string value)
    {
        return !string.IsNullOrEmpty(value);
    }
}