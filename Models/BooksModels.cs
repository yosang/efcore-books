namespace books.models;

public class Book
{
    public int ID { get; set; }
    public string Title { get; set; } = string.Empty;

    public int CategoryId { get; set; } // CategoryFK so we can add it by its int id
    public Category Category { get; set; } = null!; // one-to-one navigation prop (single)
    public List<Author> Authors { get; set; } = null!; // one-to-many navigation prop (collection)
}

public class Category
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Book> Books { get; set; } = null!; // non-nullable (will not be null)
}

public class Author
{
    public int ID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public List<Book> Books { get; set; } = null!;
}