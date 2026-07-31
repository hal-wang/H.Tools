using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace H.Tools.Asp.Filters;

public class ValidateFilter : IAlwaysRunResultFilter
{
    public void OnResultExecuted(ResultExecutedContext context)
    {
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
        var modelState = context.ModelState;
        if (modelState.IsValid)
        {
            return;
        }

        var state = modelState
            .Select(item => item.Value)
            .LastOrDefault(item => item != null && item.Errors.Count > 0);
        if (state == default)
        {
            return;
        }

        context.Result = new BadRequestObjectResult(new
        {
            message = state.Errors.LastOrDefault()?.ErrorMessage ?? ""
        });
    }
}
