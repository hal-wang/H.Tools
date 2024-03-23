using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace H.Tools.Asp.Filters;

public class FormatMessageFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        var result = context.Result;
        if (result is ObjectResult objectResult && objectResult.Value != null)
        {
            var value = objectResult.Value;
            if (value.GetType() == typeof(string))
            {
                objectResult.Value = new
                {
                    message = value
                };
            }
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }
}
