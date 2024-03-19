using System.Net;

namespace H.Tools.Asp.Exceptions;

public class TooManyRequestsException(string? message = "Too Many Requests") : HttpRequestException(message, null, HttpStatusCode.TooManyRequests)
{
}
