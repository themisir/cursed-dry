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