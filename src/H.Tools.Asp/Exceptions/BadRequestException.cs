using System.Net;

namespace H.Tools.Asp.Exceptions;

public class BadRequestException(string? message = "Bad Request") : HttpRequestException(message, null, HttpStatusCode.BadRequest), IMessageException
{
    public string? MessageType { get; set; }
    public string? MessageTitle { get; set; }
    public string? Next { get; set; }
    public string? MessageButtonText { get; set; }
}
