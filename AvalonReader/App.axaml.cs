using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvalonReader.Helpers;
using AvalonReader.Services;
using AvalonReader.ViewModels;
using AvalonReader.Views;
using System.Linq;

namespace AvalonReader
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                DisableAvaloniaDataAnnotationValidation();
                desktop.MainWindow = new MainView
                {
                    DataContext = new MainViewModel(new DialogService()),
                };
                desktop.Exit += (_, _) => AppViewModel.Instance.Settings.SaveAsync();
                TopLevel topLevel = TopLevel.GetTopLevel(desktop.MainWindow);
                GlobalSingletonHelper.StorageProvider = topLevel.StorageProvider;
            }
            base.OnFrameworkInitializationCompleted();
        }

        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            DataAnnotationsValidationPlugin[] dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (DataAnnotationsValidationPlugin plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }
    }
}