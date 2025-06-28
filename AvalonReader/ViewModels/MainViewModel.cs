using Avalonia.Platform.Storage;
using AvaloniaEdit.Document;
using AvalonReader.Helpers;
using AvalonReader.Messengers;
using AvalonReader.Services;
using AvalonReader.Settings;
using AvalonReader.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AvalonReader.ViewModels
{
    internal partial class MainViewModel : ViewModelBase, IRecipient<StatusMessenger>
    {
        private static AppSettings Settings => AppViewModel.Instance.Settings;
        private readonly IDialogService _dialogService;

        public MainViewModel() { }

        internal MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            WeakReferenceMessenger.Default.Register(this);
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsEditEnabled))]
        internal TextDocument content;

        private bool IsEditEnabled => Content != null && !string.IsNullOrWhiteSpace(Content.Text);

        [ObservableProperty]
        internal string statusText = "Ready";

        private IStorageFile OpenedFile;

        [RelayCommand]
        private async Task OpenFile()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            IStorageProvider provider = GlobalSingletonHelper.StorageProvider;
            IReadOnlyList<IStorageFile> resultFile = await provider.OpenFilePickerAsync(new FilePickerOpenOptions()
            {
                AllowMultiple = false,
                Title = "Open File"
            });
            if (resultFile.Count > 0)
            {
                await LoadFile(resultFile[0]);
            }
            else
            {
                StatusText = "No file selected.";
            }
        }

        [RelayCommand]
        private async Task ReloadFile()
        {
            await LoadFile(OpenedFile);
        }

        private async Task LoadFile(IStorageFile file)
        {
            if (file != null)
            {
                OpenedFile = file;
                await using Stream stream = await OpenedFile.OpenReadAsync();
                using StreamReader reader = new(stream, Encoding.GetEncoding(Settings.EncodingName));
                Content = new TextDocument(MergeEmptyLines(await reader.ReadToEndAsync()));
                StatusText = $"Opened: {OpenedFile.Name}";
            }
            else
            {
                StatusText = "No file selected.";
            }
        }

        private string MergeEmptyLines(string text)
        {
            return string.Join(Environment.NewLine, text.Split(["\r\n", "\n", "\r"], StringSplitOptions.RemoveEmptyEntries));
        }

        [RelayCommand]
        private void CloseFile()
        {
            Content = null;
            StatusText = "File closed.";
        }

        [RelayCommand]
        private void OpenPreferences()
        {
            _dialogService.ShowDialog(typeof(PreferencesView), new PreferencesViewModel(this, _dialogService));
        }

        public void Receive(StatusMessenger message)
        {
            if (message != null)
            {
                StatusText = message.Content;
            }
        }
    }
}
