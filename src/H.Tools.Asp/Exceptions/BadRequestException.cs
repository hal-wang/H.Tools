using System.Net;

namespace H.Tools.Asp.Exceptions;

public class BadRequestException(string? message = "Bad Request") : HttpRequestException(message, null, HttpStatusCode.BadRequest)
{
}
