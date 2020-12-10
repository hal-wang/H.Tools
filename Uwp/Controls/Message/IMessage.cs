namespace HTools.Uwp.Controls.Message
{
    internal interface IMessage
    {
        MessageType MessageType { get; set; }
        string Text { get; set; }
    }
}
