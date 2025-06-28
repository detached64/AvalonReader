using AvalonReader.Models;
using AvalonReader.ViewModels;
using System.Collections.ObjectModel;

namespace AvalonReader.Helpers
{
    internal static class RecentFileHelper
    {
        private const int MaxFiles = 10;
        private static ObservableCollection<RecentFile> Files => AppViewModel.Instance.Settings.RecentFiles;

        public static void AddFile(RecentFile file)
        {
            Files.Remove(file);

            Files.Insert(0, file);

            while (Files.Count > MaxFiles)
            {
                Files.RemoveAt(Files.Count - 1);
            }
        }

        public static void Clear()
        {
            Files.Clear();
        }
    }
}
