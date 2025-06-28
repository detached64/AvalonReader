using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Collections.Generic;

namespace AvalonReader.Services
{
    public sealed class DialogService : IDialogService
    {
        private readonly Dictionary<object, Window> _openWindows = [];

        public void ShowDialog(Type viewType, object viewModel)
        {
            if (!typeof(Window).IsAssignableFrom(viewType))
            {
                throw new ArgumentException("The provided type must be a Window.", nameof(viewType));
            }

            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                Window window = (Window)Activator.CreateInstance(viewType)!;
                window.DataContext = viewModel;
                window.Closed += (s, e) => _openWindows.Remove(viewModel);

                _openWindows[viewModel] = window;
                window.ShowDialog(desktop.MainWindow);
            }
            else
            {
                throw new InvalidOperationException("DialogService can only be used in a desktop application lifetime.");
            }
        }

        public void CloseDialog(object viewModel)
        {
            if (Application.Current.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime)
            {
                throw new InvalidOperationException("DialogService can only be used in a desktop application lifetime.");
            }
            if (_openWindows.TryGetValue(viewModel, out Window window))
            {
                window.Close();
                _openWindows.Remove(viewModel);
            }
        }
    }
}
