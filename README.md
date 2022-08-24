# Abp Extend Enumeration Class
### Project

#### Install Abp CLI

```bash
dotnet tool install -g Volo.Abp.Cli
```

#### Build

Open the root directory of the project and run the following command for init database structure,basic data and install lib

``` bash
.\build.ps1
```



#### Run

run host and blazor project

#### Login

1. UserName: admin
2. Password: 1q2w3E*



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



### JsonConverter

#### Newtonsoft

``` C#
 public class EnumerationClassNewtonsoftJsonConverter<TEnumeration>
        : JsonConverter<TEnumeration>
        where TEnumeration : Enumeration
        {

        }
```

#### System.Text.Json

``` C#
public class EnumerationClassSystemTextJsonConverter<TEnumeration>
        : JsonConverter<TEnumeration>
        where TEnumeration : Enumeration
        {

        }
```

#### JsonConverterAttribute Usage

``` C#
 public class BookDto
 {
     [Newtonsoft.Json.JsonConverter(typeof(EnumerationClassNewtonsoftJsonConverter<BookEnumerationType>))]
     [System.Text.Json.Serialization.JsonConverter(typeof(EnumerationClassSystemTextJsonConverter<BookEnumerationType>))]
     public BookEnumerationType Type2 { get; set; }
 }

 ....
     
 public class CreateUpdateBookDto
 {
     [Newtonsoft.Json.JsonConverter(typeof(EnumerationClassNewtonsoftJsonConverter<BookEnumerationType>))]
     [System.Text.Json.Serialization.JsonConverter(typeof(EnumerationClassSystemTextJsonConverter<BookEnumerationType>))]
     public BookEnumerationType Type2 { get; set; } = BookEnumerationType.Undefined;
 }
```

 

You can use dependency injection instead of JsonConverterAttribute

#### DependencyInjection Usage

``` C#
....Dto Part....
public class BookDto
{
    public BookEnumerationType Type2 { get; set; }
}

....DI Part....
Configure<JsonOptions>(options =>
{   
    options.JsonSerializerOptions.AddEnumerationJsonConverters(typeof(BookEnumerationType));
    options.JsonSerializerOptions.AddEnumerationJsonConverters(typeof(...));
);
```



### Test Case

#### System.Text.Json

``` bash
dotnet test .\test\Extension.Test\Extension.Test.csproj
```



### Perform
The location of the GIF file is in the docs directory
![](docs\enumerationClassEditAndSelect.gif)

### Todo List

- [x] ⭐⭐⭐⭐jsonOptions add system text jsonConverter（DI）
- [ ] ⭐⭐⭐ jsonOptions add newtonsoft jsonConverter（DI）
- [ ] ⭐⭐Book edit page support enumeration class
- [ ] ⭐⭐Book create page support enumeration class

### Reference Resources

1. basic code copy from [abpframework/abp-samples/BookStore-Blazor-EfCore](https://github.com/abpframework/abp-samples/tree/master/BookStore-Blazor-EfCore)
2. [microsoft document](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types)



