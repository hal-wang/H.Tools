using System.Net;

namespace H.Tools.Asp.Exceptions;

public class UnauthorizedException(string? message = "Unauthorized") : HttpRequestException(message, null, HttpStatusCode.Unauthorized), IMessageException
{
    public string? MessageType { get; set; }
    public string? MessageTitle { get; set; }
    public string? Next { get; set; }
    public string? MessageButtonText { get; set; }
}
