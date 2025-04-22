using System.Net;

namespace H.Tools.Asp;

public static class ErrorMessageExtend
{
    public static IHttpContextAccessor ErrorModalTitle(this IHttpContextAccessor httpContextAccessor, string title)
    {
        httpContextAccessor.HttpContext?.Response?.Headers?.TryAdd("error-message-title", WebUtility.UrlEncode(title));
        httpContextAccessor.HttpContext?.Response?.Headers?.TryAdd("error-message", "modal");
        return httpContextAccessor;
    }

    public static IHttpContextAccessor ErrorMessageTitle(this IHttpContextAccessor httpContextAccessor, string title)
    {
        httpContextAccessor.HttpContext?.Response?.Headers?.TryAdd("error-message-title", WebUtility.UrlEncode(title));
        httpContextAccessor.HttpContext?.Response?.Headers?.TryAdd("error-message", "message");
        return httpContextAccessor;
    }
}
