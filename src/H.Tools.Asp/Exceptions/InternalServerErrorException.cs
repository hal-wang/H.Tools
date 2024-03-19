using System.Net;

namespace H.Tools.Asp.Exceptions;

public class InternalServerErrorException(string? message = "Internal Server Error") : HttpRequestException(message, null, HttpStatusCode.InternalServerError)
{
}
