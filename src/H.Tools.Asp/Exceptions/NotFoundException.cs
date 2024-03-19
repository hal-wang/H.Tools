using System.Net;

namespace H.Tools.Asp.Exceptions;

public class NotFoundException(string? message = "Not Found") : HttpRequestException(message, null, HttpStatusCode.NotFound)
{
}
