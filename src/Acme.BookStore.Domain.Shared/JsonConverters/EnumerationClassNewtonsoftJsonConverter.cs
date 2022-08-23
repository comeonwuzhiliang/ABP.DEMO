using Newtonsoft.Json;
using System;

namespace Acme.BookStore.JsonConverters
{
    public class EnumerationClassNewtonsoftJsonConverter<TEnumeration>
        : JsonConverter<TEnumeration>
        where TEnumeration : Enumeration
    {
        public override bool CanRead => true;

        public override bool CanWrite => true;

        public override TEnumeration ReadJson(JsonReader reader, Type objectType, TEnumeration existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            int value = int.Parse(reader.Value.ToString());

            return Enumeration.FromValue<TEnumeration>(value);
        }

        public override void WriteJson(JsonWriter writer, TEnumeration value, JsonSerializer serializer)
        {
            if (value is null)
                writer.WriteNull();
            else
                writer.WriteValue(value.Id);
        }
    }

    //public class EnumerationClassNewtonsoftJsonConverter
    //    : JsonConverter<Enumeration>
    //{
    //    public override bool CanRead => true;

    //    public override bool CanWrite => true;

    //    public override Enumeration ReadJson(JsonReader reader, Type objectType, Enumeration existingValue, bool hasExistingValue, JsonSerializer serializer)
    //    {
    //        int value = (int)reader.Value;

    //        return Enumeration.FromValue<Enumeration>(value);
    //    }

    //    public override void WriteJson(JsonWriter writer, Enumeration value, JsonSerializer serializer)
    //    {
    //        if (value is null)
    //            writer.WriteNull();
    //        else
    //            writer.WriteValue(value.Id);
    //    }
    //}
}
