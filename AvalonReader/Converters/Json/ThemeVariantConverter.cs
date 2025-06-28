using Avalonia.Styling;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AvalonReader.Converters.Json
{
    internal class ThemeVariantConverter : JsonConverter<ThemeVariant>
    {
        public override ThemeVariant Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return value switch
            {
                "Light" => ThemeVariant.Light,
                "Dark" => ThemeVariant.Dark,
                _ => ThemeVariant.Default
            };
        }

        public override void Write(Utf8JsonWriter writer, ThemeVariant value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Key.ToString());
        }
    }
}
