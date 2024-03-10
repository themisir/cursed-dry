namespace DryApi.Demo3;

public sealed class OkValueChecker : IValueChecker
{
    public bool TestValue(string value)
    {
        return value == "ok";
    }
}