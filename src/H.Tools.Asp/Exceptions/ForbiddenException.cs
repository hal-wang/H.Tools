using System.Net;

namespace H.Tools.Asp.Exceptions;

public class ForbiddenException(string? message = "Forbidden") : HttpRequestException(message, null, HttpStatusCode.Forbidden), IMessageException
{
    public string? MessageType { get; set; }
    public string? MessageTitle { get; set; }
    public string? Next { get; set; }
    public string? MessageButtonText { get; set; }
}
