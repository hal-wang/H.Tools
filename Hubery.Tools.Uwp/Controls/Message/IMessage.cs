namespace Hubery.Tools.Uwp.Controls.Message
{
    internal interface IMessage
    {
        MessageType MessageType { get; set; }
        string Text { get; set; }
    }
}
