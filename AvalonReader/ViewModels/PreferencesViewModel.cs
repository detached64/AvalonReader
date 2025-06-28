using Avalonia.Media;
using Avalonia.Styling;
using AvalonReader.Services;
using AvalonReader.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AvalonReader.ViewModels
{
    internal partial class PreferencesViewModel : ViewModelBase
    {
        private static AppSettings Settings => AppViewModel.Instance.Settings;

        private readonly MainViewModel _parentVM;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private ObservableCollection<ThemeVariant> variants;

        [ObservableProperty]
        private ObservableCollection<FontFamily> systemFonts;

        [ObservableProperty]
        private ObservableCollection<string> encodingNames;

        public PreferencesViewModel() { }

        internal PreferencesViewModel(MainViewModel parentVM, IDialogService dialogService)
        {
            _parentVM = parentVM;
            _dialogService = dialogService;
            InitCollections();
        }

        private void InitCollections()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Variants = [ThemeVariant.Default, ThemeVariant.Light, ThemeVariant.Dark];
            SystemFonts = new ObservableCollection<FontFamily>(FontManager.Current.SystemFonts);
            EncodingNames = new ObservableCollection<string>(
                Encoding.GetEncodings()
                    .Select(e => e.GetEncoding().WebName)
                    .Append(Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.ANSICodePage).WebName)
                    .Distinct()
                    .Order()
            );
        }
    }
}
