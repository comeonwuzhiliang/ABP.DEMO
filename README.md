# Extened Enumeration Class
### Enumeration Class

#### Base Class

```C#
public abstract class Enumeration : IComparable
{
    public string Name { get; private set; }

    public int Id { get; private set; }

    protected Enumeration(int id, string name) => (Id, Name) = (id, name);

    public override string ToString() => Name;

    public static T FromValue<T>(int value) where T : Enumeration
    {
        var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
        return matchingItem;
    }
        
    ...
}
```

#### Implementation Class

``` C#
public class BookEnumerationType: Enumeration
{
    public static BookEnumerationType Undefined = new(0, nameof(Undefined));
    public static BookEnumerationType Adventure = new(1, nameof(Adventure));
    public static BookEnumerationType Biography = new(2, nameof(Biography));
    public static BookEnumerationType Dystopia = new(3, nameof(Dystopia));
    public static BookEnumerationType Fantastic = new(4, nameof(Fantastic));
    public static BookEnumerationType Horror = new(5, nameof(Horror));
    public static BookEnumerationType Science = new(6, nameof(Science));
    public static BookEnumerationType ScienceFiction = new(7,nameof(ScienceFiction));
    public static BookEnumerationType Poetry = new(8, nameof(Poetry));

    public BookEnumerationType(int id, string name)
        : base(id, name)
        {
        }
}
```



### EntityFramework Core

#### Db Table Entity

    public class Book : AuditedAggregateRoot<Guid>
    {
        public Guid AuthorId { get; set; }
    
        public string Name { get; set; }
    
        public BookType Type { get; set; }
    
        public BookEnumerationType Type2 { get; set; }
    
        public DateTime PublishDate { get; set; }
    
        public float Price { get; set; }
    }

#### DbContext

``` C#

protected override void OnModelCreating(ModelBuilder builder)
 {
     ...
     
     //ignore therImplementation Class if you not need generate db table
     builder.Ignore(typeof(BookEnumerationType)); 
    
     b.Property(x => x.Type2)
      .HasConversion(x => x.Id, x => Enumeration.FromValue<BookEnumerationType>(x));
 }
```







### Reference Resources

1. basic code copy from [abpframework/abp-samples/BookStore-Mvc-EfCore](https://github.com/abpframework/abp-samples/tree/master/BookStore-Mvc-EfCore)
2. [microsoft document](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types)



