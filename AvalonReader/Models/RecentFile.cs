using Avalonia.Platform.Storage;

namespace AvalonReader.Models
{
    internal sealed class RecentFile
    {
        public IStorageFile File { get; set; }
        public string Path => File.Path.LocalPath;
    }
}
