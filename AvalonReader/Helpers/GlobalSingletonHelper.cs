using Avalonia.Platform.Storage;

namespace AvalonReader.Helpers
{
    internal static class GlobalSingletonHelper
    {
        public static IStorageProvider StorageProvider { get; set; }
    }
}
