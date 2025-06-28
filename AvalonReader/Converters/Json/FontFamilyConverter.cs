using Avalonia.Media;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AvalonReader.Converters.Json
{
    internal class FontFamilyConverter : JsonConverter<FontFamily>
    {
        public override FontFamily Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new FontFamily(reader.GetString() ?? "Segoe UI");
        }

        public override void Write(Utf8JsonWriter writer, FontFamily value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Name);
        }
    }
}