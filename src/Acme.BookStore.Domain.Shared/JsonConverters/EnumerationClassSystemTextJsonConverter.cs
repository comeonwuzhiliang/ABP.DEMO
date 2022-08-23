using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Acme.BookStore.JsonConverters
{
    public class EnumerationClassSystemTextJsonConverter<TEnumeration>
        : JsonConverter<TEnumeration>
        where TEnumeration : Enumeration
    {
        public override TEnumeration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            return Enumeration.FromValue<TEnumeration>(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, TEnumeration value, JsonSerializerOptions options)
        {
            if (value == null)
                writer.WriteNullValue();

            writer.WriteNumberValue(value.Id);
        }
    }
}
