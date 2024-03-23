using System.Net;

namespace H.Tools.Asp.Exceptions;

public class InternalServerErrorException(string? message = "Internal Server Error") : HttpRequestException(message, null, HttpStatusCode.InternalServerError), IMessageException
{
    public string? MessageType { get; set; }
    public string? MessageTitle { get; set; }
    public string? Next { get; set; }
    public string? MessageButtonText { get; set; }
}
