using Acme.BookStore.JsonConverters;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Books
{
    public class BookDto : AuditedEntityDto<Guid>
    {
        public Guid AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string Name { get; set; }

        public BookType Type { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(EnumerationClassNewtonsoftJsonConverter<BookEnumerationType>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(EnumerationClassSystemTextJsonConverter<BookEnumerationType>))]
        public BookEnumerationType Type2 { get; set; }

        public int Type2Value => Type2?.Id ?? default;

        public string Type2Name => Type2?.Name ?? default;

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}
