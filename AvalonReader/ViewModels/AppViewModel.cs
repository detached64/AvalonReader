using AvalonReader.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AvalonReader.ViewModels
{
    internal partial class AppViewModel : ObservableObject
    {
        private static readonly Lazy<AppViewModel> _instance =
            new(() => new AppViewModel());

        private AppViewModel()
        {
            Settings = AppSettings.Load();
        }

        public static AppViewModel Instance => _instance.Value;

        public AppSettings Settings { get; private set; }
    }
}
