using Avalonia;
using Avalonia.Media;
using Avalonia.Styling;
using AvalonReader.Converters.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace AvalonReader.Settings
{
    internal partial class AppSettings : ObservableObject
    {
        [ObservableProperty]
        private FontFamily fontFamily = new("Segoe UI");

        [ObservableProperty]
        private double fontSize = 20;

        [ObservableProperty]
        private string encodingName = "utf-8";

        [ObservableProperty]
        private bool wrapText = true;

        [ObservableProperty]
        private ThemeVariant themeVariant = ThemeVariant.Default;

        private static string SettingsPath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            Assembly.GetExecutingAssembly().GetName().Name,
            "settings.json"
        );

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new ThemeVariantConverter(),
                new FontFamilyConverter(),
                new FontSizeConverter(),
                new EncodingConverter(),
            }
        };

        public async void SaveAsync()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath));
                await using FileStream stream = File.Create(SettingsPath);
                await JsonSerializer.SerializeAsync(stream, this, JsonOptions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving settings: {ex.Message}");
            }
        }

        public static AppSettings Load()
        {
            try
            {
                if (File.Exists(SettingsPath))
                {
                    string json = File.ReadAllText(SettingsPath);
                    return JsonSerializer.Deserialize<AppSettings>(json, JsonOptions) ?? new AppSettings();
                }
                else
                {
                    return new AppSettings();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading settings: {ex.Message}");
                return new AppSettings();
            }
        }

        partial void OnThemeVariantChanged(ThemeVariant value)
        {
            Application.Current.RequestedThemeVariant = value;
        }
    }
}
