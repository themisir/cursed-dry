using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DryApi.Demo5;

[AttributeUsage(AttributeTargets.Method)]
public sealed class ValueCheckerFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var value = context.HttpContext.Request.Query["value"];
        var otherValue = context.HttpContext.Request.Query["other"];

        if (!value.Equals(otherValue))
        {
            context.Result = new BadRequestResult();
        }
    }
}