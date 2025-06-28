using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AvalonReader.Converters.Json
{
    internal class FontSizeConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            double val = reader.GetDouble();
            if (val < 8 || val > 72)
            {
                val = 20;
            }
            return val;
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(Convert.ToDecimal(Math.Round(value, 2, MidpointRounding.AwayFromZero)));
        }
    }
}
