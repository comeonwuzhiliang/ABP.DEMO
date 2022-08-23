namespace Acme.BookStore.Books
{
    public enum BookType
    {
        Undefined,
        Adventure,
        Biography,
        Dystopia,
        Fantastic,
        Horror,
        Science,
        ScienceFiction,
        Poetry
    }

    public class BookEnumerationType
    : Enumeration
    {
        public static BookEnumerationType Undefined = new(0, nameof(Undefined));
        public static BookEnumerationType Adventure = new(1, nameof(Adventure));
        public static BookEnumerationType Biography = new(2, nameof(Biography));
        public static BookEnumerationType Dystopia = new(3, nameof(Dystopia));
        public static BookEnumerationType Fantastic = new(4, nameof(Fantastic));
        public static BookEnumerationType Horror = new(5, nameof(Horror));
        public static BookEnumerationType Science = new(6, nameof(Science));
        public static BookEnumerationType ScienceFiction = new(7, nameof(ScienceFiction));
        public static BookEnumerationType Poetry = new(8, nameof(Poetry));

        //public override string ToString() => Id.ToString();
        public BookEnumerationType(int id, string name)
            : base(id, name)
        {
        }
    }
}
