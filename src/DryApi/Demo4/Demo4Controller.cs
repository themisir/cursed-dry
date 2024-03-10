using Microsoft.AspNetCore.Mvc;

namespace DryApi.Demo4;

[ApiController, Route("[controller]")]
public sealed class Demo4Controller : ControllerBase
{
    private readonly ValueCheckerFactory _valueCheckerFactory;

    public Demo4Controller(ValueCheckerFactory valueCheckerFactory)
    {
        _valueCheckerFactory = valueCheckerFactory;
    }

    [HttpGet]
    public ActionResult Index(string value, string other)
    {
        if (_valueCheckerFactory.CreateChecker(other).TestValue(value))
        {
            return Ok("pass");
        }
        return BadRequest();
    }
}