using Acme.BookStore;
using Acme.BookStore.Books;
using Acme.BookStore.JsonConverters;
using NewtonsoftJsonConverter = Newtonsoft.Json.JsonConverterAttribute;
using SystemTextJsonConverter = System.Text.Json.Serialization.JsonConverterAttribute;

namespace Extension.Test.JsonConverter.Fake
{
    public record LogDto
    {
        public string? Content { get; set; }

        [SystemTextJsonConverter(typeof(EnumerationClassSystemTextJsonConverter<LogType>))]
        [NewtonsoftJsonConverter(typeof(EnumerationClassNewtonsoftJsonConverter<LogType>))]
        public LogType? Type { get; set; }
    }

    public class LogType
   : Enumeration
    {
        public static LogType Warn = new(1, nameof(Warn));
        public static LogType Information = new(2, nameof(Information));
        public static LogType Error = new(3, nameof(Error));

        public LogType(int id, string name)
            : base(id, name)
        {
        }
    }
}
