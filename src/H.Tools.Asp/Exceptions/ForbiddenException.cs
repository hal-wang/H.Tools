using System.Net;

namespace H.Tools.Asp.Exceptions;

public class ForbiddenException(string? message = "Forbidden") : HttpRequestException(message, null, HttpStatusCode.Forbidden)
{
}
