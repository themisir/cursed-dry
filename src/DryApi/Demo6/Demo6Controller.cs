using Microsoft.AspNetCore.Mvc;

namespace DryApi.Demo6;

[ApiController, Route("[controller]")]
public sealed class Demo6Controller : ControllerBase
{
    [HttpGet]
    [RequestFilter("""ctx.Request.Query["value"] == ctx.Request.Query["other"]""")]
    public ActionResult Index()
    {
        return Ok("pass");
    }
}