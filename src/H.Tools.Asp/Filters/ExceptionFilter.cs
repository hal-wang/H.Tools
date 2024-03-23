using H.Tools.Asp.Exceptions;
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

        var status = exception.StatusCode == null ? 500 : (int)exception.StatusCode;
        if (context.Exception is IMessageException messageException)
        {
            var objectResult = new ObjectResult(new
            {
                Message = string.IsNullOrEmpty(messageException.Message) ? "Unknown Error" : messageException.Message,
                messageException.MessageTitle,
                messageException.MessageType,
                messageException.MessageButtonText,
                messageException.Next,
            })
            {
                StatusCode = status,
            };
            context.Result = objectResult;
        }
        else
        {
            var message = context.Exception.Message;
            var objectResult = new ObjectResult(new
            {
                Message = string.IsNullOrEmpty(message) ? "Unknown Error" : message,
            })
            {
                StatusCode = status,
            };
            context.Result = objectResult;
        }
    }
}
