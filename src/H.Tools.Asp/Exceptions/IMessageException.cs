namespace H.Tools.Asp.Exceptions;

public interface IMessageException
{
    public string Message { get; }
    public string? MessageType { get; }
    public string? MessageTitle { get; }
    public string? Next { get; }
    public string? MessageButtonText { get; }
}
