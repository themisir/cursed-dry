using Microsoft.AspNetCore.Mvc;

namespace DryApi.Demo3;

[ApiController, Route("[controller]")]
public sealed class Demo3Controller : ControllerBase
{
    private readonly IValueChecker _valueChecker;

    public Demo3Controller(IValueChecker valueChecker)
    {
        _valueChecker = valueChecker;
    }

    [HttpGet]
    public ActionResult Index(string value)
    {
        if (_valueChecker.TestValue(value))
        {
            return Ok("pass");
        }
        return BadRequest();
    }
}

public interface IValueChecker
{
    bool TestValue(string value);
}

public sealed class OkValueChecker : IValueChecker
{
    public bool TestValue(string value)
    {
        return value == "ok";
    }
}

public sealed class NonEmptyValueChecker : IValueChecker
{
    public bool TestValue(string value)
    {
        return !string.IsNullOrEmpty(value);
    }
}

public enum ValueCheckerType
{
    Ok,
    NonEmpty
}

public static class Demo3ServiceExtensions
{
    public static IServiceCollection AddDemo3Services(this IServiceCollection services, IConfiguration configuration)
    {
        return AddDemo3Services(services, configuration.GetValue<ValueCheckerType>("ValueChecker"));
    }

    public static IServiceCollection AddDemo3Services(this IServiceCollection services, ValueCheckerType valueCheckerType)
    {
        switch (valueCheckerType)
        {
            case ValueCheckerType.Ok:
                services.AddSingleton<IValueChecker, OkValueChecker>();
                break;
            case ValueCheckerType.NonEmpty:
                services.AddSingleton<IValueChecker, NonEmptyValueChecker>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(valueCheckerType), valueCheckerType, null);
        }
        return services;
    }
}