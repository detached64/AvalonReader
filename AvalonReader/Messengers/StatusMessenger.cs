namespace AvalonReader.Messengers
{
    internal sealed class StatusMessenger(string content)
    {
        public string Content { get; set; } = content;
    }
}
