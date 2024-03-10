using Microsoft.AspNetCore.Mvc;

namespace DryApi.Demo2;

[ApiController, Route("[controller]")]
public sealed class Demo2Controller : ControllerBase
{
    private readonly ValueChecker _valueChecker;

    public Demo2Controller(ValueChecker valueChecker)
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

public sealed class ValueChecker
{
    public bool TestValue(string value)
    {
        return value == "ok";
    }
}

public static class Demo2ServiceExtensions
{
    public static IServiceCollection AddDemo2Services(this IServiceCollection services)
    {
        services.AddSingleton<ValueChecker>();
        return services;
    }
}