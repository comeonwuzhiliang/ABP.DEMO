using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Books
{
    public class BookDto : AuditedEntityDto<Guid>
    {
        public Guid AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string Name { get; set; }

        public BookType Type { get; set; }

        public int Type2 { get; set; }

        public string Type2Name
        {
            get
            {
                if (Type2 < 0)
                {
                    return string.Empty;
                }
                else
                {
                    return Enumeration.FromValue<BookEnumerationType>(Type2).Name;
                }
            }
        }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}
