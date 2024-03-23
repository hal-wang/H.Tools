using System.Net;

namespace H.Tools.Asp.Exceptions;

public class NotFoundException(string? message = "Not Found") : HttpRequestException(message, null, HttpStatusCode.NotFound), IMessageException
{
    public string? MessageType { get; set; }
    public string? MessageTitle { get; set; }
    public string? Next { get; set; }
    public string? MessageButtonText { get; set; }
}
