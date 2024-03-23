using System.Net;

namespace H.Tools.Asp.Exceptions;

public class TooManyRequestsException(string? message = "Too Many Requests") : HttpRequestException(message, null, HttpStatusCode.TooManyRequests), IMessageException
{
    public string? MessageType { get; set; }
    public string? MessageTitle { get; set; }
    public string? Next { get; set; }
    public string? MessageButtonText { get; set; }
}
