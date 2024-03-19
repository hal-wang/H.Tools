using System.Net;

namespace H.Tools.Asp.Exceptions;

public class UnauthorizedException(string? message = "Unauthorized") : HttpRequestException(message, null, HttpStatusCode.Unauthorized)
{
}
