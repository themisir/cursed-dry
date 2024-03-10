using Microsoft.AspNetCore.Mvc;

namespace DryApi.Demo1;

[ApiController, Route("[controller]")]
public sealed class Demo1Controller : ControllerBase
{
    [HttpGet]
    public ActionResult Index(string value)
    {
        if (value == "ok")
        {
            return Ok("pass");
        }
        return BadRequest();
    }
}