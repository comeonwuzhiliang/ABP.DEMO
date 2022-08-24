using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.BookStore;
using Acme.BookStore.Books;
using Acme.BookStore.JsonConverters;
using Extension.Test.JsonConverter.Fake;

namespace Extension.Test.JsonConverter
{
    public class SystemTextJsonConverterTest
    {
        [Fact]
        public void SystemTextJsonConverter()
        {
            LogDto logDto = new LogDto { Content = "Successful", Type = LogType.Information };

            string logDtoStr = System.Text.Json.JsonSerializer.Serialize(logDto);

            IDictionary<string, object>? logDtoDic = System.Text.Json.JsonSerializer.Deserialize<IDictionary<string, object>>(logDtoStr);

            var typeValue = logDtoDic!["Type"];

            Assert.Equal(2, int.Parse(typeValue!.ToString()!));

            string logDtoTagStr = "{\"Content\":\"Successful\",\"Type\":2}";

            LogDto? logDto2 = System.Text.Json.JsonSerializer.Deserialize<LogDto>(logDtoTagStr);

            Assert.Equal(logDto2, logDto);

            LogDto logDto3 = logDto with { };

            Assert.Equal(logDto2, logDto3);
        }
    }
}
