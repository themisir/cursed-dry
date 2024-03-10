using Microsoft.AspNetCore.Mvc;

namespace DryApi.Demo5;

[ApiController, Route("[controller]")]
public sealed class Demo5Controller : ControllerBase
{
    [HttpGet]
    [ValueCheckerFilter]
    public ActionResult Index()
    {
        return Ok("pass");
    }
}