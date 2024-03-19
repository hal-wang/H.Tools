using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace H.Tools.Asp.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not HttpRequestException exception)
        {
            return;
        }

        context.ExceptionHandled = true;

        context.HttpContext.Response.Headers.TryAdd("source-exception", exception.GetType().ToString());
        var message = context.Exception.Message;
        var objectResult = new ObjectResult(new
        {
            message = string.IsNullOrEmpty(message) ? "Unknown Error" : message,
        })
        {
            StatusCode = exception.StatusCode == null ? 500 : (int)exception.StatusCode,
        };
        context.Result = objectResult;
    }
}
