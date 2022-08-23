using Acme.BookStore.JsonConverters;
using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Books
{
    public class CreateUpdateBookDto
    {
        public Guid AuthorId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public BookType Type { get; set; } = BookType.Undefined;

        [Newtonsoft.Json.JsonConverter(typeof(EnumerationClassNewtonsoftJsonConverter<BookEnumerationType>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(EnumerationClassSystemTextJsonConverter<BookEnumerationType>))]
        public BookEnumerationType Type2 { get; set; } = BookEnumerationType.Undefined;

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        [Required]
        public float Price { get; set; }
    }
}
