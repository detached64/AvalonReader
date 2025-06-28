using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AvalonReader.Converters.Json
{
    internal class EncodingConverter : JsonConverter<Encoding>
    {
        public override Encoding Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Encoding.GetEncoding(reader.GetString() ?? "utf-8");
        }

        public override void Write(Utf8JsonWriter writer, Encoding value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.WebName);
        }
    }
}
